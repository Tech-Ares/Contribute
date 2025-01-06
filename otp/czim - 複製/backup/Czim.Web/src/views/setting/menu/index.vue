<template>
	<el-container>
		<el-aside width="300px" v-loading="menuloading">
			<el-container>
				<el-header>
					<el-input placeholder="输入关键字进行过滤" v-model="menuFilterText" clearable></el-input>
				</el-header>
				<el-main class="nopadding">
					<el-tree ref="menu" class="menu" node-key="id" :data="menuList" :props="menuProps" draggable highlight-current :expand-on-click-node="false" check-strictly show-checkbox :filter-node-method="menuFilterNode" @node-click="menuClick" @node-drop="nodeDrop">

						<template #default="{node, data}">
							<span class="custom-tree-node el-tree-node__label">
								<span class="label">
									{{ node.label }}
								</span>
								<span class="do" v-if="node.level < 2">
									<el-icon @click.stop="add(node, data)"><el-icon-plus /></el-icon>
								</span>
							</span>
						</template>

					</el-tree>
				</el-main>
				<el-footer style="height:51px;">
					<el-button type="primary" size="mini" icon="el-icon-plus" @click="add()"></el-button>
					<el-button type="danger" size="mini" plain icon="el-icon-delete" @click="delMenu"></el-button>
				</el-footer>
			</el-container>
		</el-aside>
		<el-container>
			<el-main class="nopadding" style="padding:20px;" ref="main">
				<save ref="save" :menu="menuList" @on-save="updateMenu"></save>
			</el-main>
		</el-container>
	</el-container>
</template>

<script>
	let newMenuIndex = 1;
	import save from './save'

	export default {
		name: "settingMenu",
		components: {
			save
		},
		data(){
			return {
				menuloading: false,
				menuList: [],
				menuProps: {
					label: (data)=>{
						return data.meta.title
					}
				},
				menuFilterText: "",
				temporaryId: ""
			}
		},
		watch: {
			menuFilterText(val){
				this.$refs.menu.filter(val);
			}
		},
		mounted() {
			this.getMenu();
		},
		methods: {
			//加载菜单数据
			async getMenu(){
				this.menuloading = true
				var res = await this.$API.sysMenu.menuList.get();
				this.menuloading = false
				this.menuList = res.data;
				console.log(res.data);
			},
			//树点击
			menuClick(data){
				// var pid = node.level==1?undefined:node.parent.data.id;
				// this.$refs.save.setData(data, pid)
				this.$refs.save.setData(data)
				this.$refs.main.$el.scrollTop = 0
			},
			//树过滤
			menuFilterNode(value, data){
				if (!value) return true;
				var targetText = data.meta.title;
				return targetText.indexOf(value) !== -1;
			},
			//树拖拽
			nodeDrop(draggingNode, dropNode, dropType){
				this.$refs.save.setData({})
				this.$message(`拖拽对象：${draggingNode.data.meta.title}, 释放对象：${dropNode.data.meta.title}, 释放对象的位置：${dropType}`)
			},
			// 随机数方法，在新增菜单中调用
			randomString(len) {
				len = len || 10;
				let $chars =
					"ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678"; /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/
				let maxPos = $chars.length;
				let id = "linshi-";
				for (let i = 0; i < len; i++) {
					id += $chars.charAt(Math.floor(Math.random() * maxPos));
				}
				return id;
			},
			//增加
			async add(node, data){
				console.log('node', node);
				console.log('data', data);
				// 前端生成临时菜单id
				this.temporaryId  = this.randomString();
				// console.log(temporaryId);
				// 将临时菜单添加到页面中
				var newMenuName = "未命名" + newMenuIndex++;
				var newMenuData = {
					parentId: data ? data.id : "",
					name: newMenuName,
					number: 0,
					path: "",
					component: "",
					// auth: [],
					meta:{
						title: newMenuName,
						type: "menu"
					},
					id: this.temporaryId
				}
				this.menuloading = true
				// var res = await this.$API.demo.post.post(newMenuData)
				this.menuloading = false
				// newMenuData.id = res.data

				this.$refs.menu.append(newMenuData, node)
				this.$refs.menu.setCurrentKey(newMenuData.id)
				// var pid = node ? node.data.id : ""
				// this.$refs.save.setData(newMenuData, pid)
				this.$refs.save.setData(newMenuData)
			},
			//删除菜单
			async delMenu(){
				// 获取选中的节点
				var CheckedNodes = this.$refs.menu.getCheckedNodes()
				console.log('选中的nodes:', CheckedNodes);
				if(CheckedNodes.length == 0){
					this.$message.warning("请选择需要删除的项")
					return false;
				}

				var confirm = await this.$confirm('确认删除已选择的菜单吗？','提示', {
					type: 'warning',
					confirmButtonText: '删除',
					confirmButtonClass: 'el-button--danger'
				}).catch(() => {})
				if(confirm != 'confirm'){
					return false
				}

				this.menuloading = true
				var reqData = CheckedNodes.map(item => item.id)
				var res = await this.$API.sysMenu.menuDelete.post(reqData)
				this.menuloading = false

				console.log(res);
				if (res.code !== 0) {
					return this.$message.warning("删除失败")
				}
				this.getMenu();
				this.$refs.save.setData({})
				return this.$message.success("删除菜单成功")
				// if(res.code == 200){
				// 	CheckedNodes.forEach(item => {
				// 		var node = this.$refs.menu.getNode(item)
				// 		if(node.isCurrent){
				// 			this.$refs.save.setData({})
				// 		}
				// 		this.$refs.menu.remove(item)
				// 	})
				// }else{
				// 	this.$message.warning(res.message)
				// }
			},
			// 新增完成后，更新菜单 和 当前选中项
			async updateMenu (id) {
				await this.getMenu();
				console.log(id);
				this.$refs.menu.setCurrentKey(id)
				let newData = this.$refs.menu.getCurrentNode();
				this.$refs.save.setData(newData);
			}
		}
	}
</script>

<style scoped>
	.custom-tree-node {display: flex;flex: 1;align-items: center;justify-content: space-between;font-size: 14px;padding-right: 24px;height:100%;}
	.custom-tree-node .label {display: flex;align-items: center;;height: 100%;}
	.custom-tree-node .label .el-tag {margin-left: 5px;}
	.custom-tree-node .do {display: none;}
	.custom-tree-node .do i {margin-left:5px;color: #999;padding:5px;}
	.custom-tree-node .do i:hover {color: #333;}

	.custom-tree-node:hover .do {display: inline-block;}
</style>
