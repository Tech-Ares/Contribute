<template>
	<el-dialog :title="titleMap[mode]" v-model="visible" :width="500" destroy-on-close @closed="$emit('closed')">
		<el-form :model="form" :rules="rules" ref="dialogForm" label-width="100px" label-position="left" v-loading="loading">
			<el-form-item label="角色名称" prop="name">
				<el-input v-model="form.name" clearable></el-input>
			</el-form-item>
			<el-form-item label="排序" prop="number">
				<el-input-number v-model="form.number" controls-position="right" :min="1" style="width: 100%;"></el-input-number>
			</el-form-item>
			<el-form-item label="说明" prop="remark">
				<el-input v-model="form.remark" clearable></el-input>
			</el-form-item>
		</el-form>
		<template #footer>
			<el-button @click="visible=false" >取 消</el-button>
			<el-button type="primary" :loading="isSaveing" @click="submit()">保 存</el-button>
		</template>
	</el-dialog>
</template>

<script>
	export default {
		emits: ['success', 'closed'],
		data() {
			return {
				mode: "add",
				titleMap: {
					edit: '编辑',
					add: '新增'
				},
				visible: false,
				isSaveing: false,
				//表单数据
				form: {
					name: "",
					number: 1,
					remark: "",
					id: ""
				},
				roleId: '',
				loading: false,
				//验证规则
				rules: {
					number: [
						{required: true, message: '请输入排序', trigger: 'change'}
					],
					name: [
						{required: true, message: '请输入角色名称'}
					],
					remark: [
						{required: true, message: '请输入角色说明'}
					]
				},
				//所需数据选项
				groups: [],
				groupsProps: {
					value: "id",
					emitPath: false,
					checkStrictly: true
				}
			}
		},
		// mounted() {
		// 	this.getRoleData({ id: this.roleId })
		// },
		methods: {
			//显示
			open(mode='add'){
				this.mode = mode;
				this.visible = true;
				return this
			},
			//加载树数据
			async getRoleData(params){
				this.loading = true;
				const res = await this.$API.sysRole.getRoleById.get(params);
				console.log(res);
				this.loading = false;
				// this.groups = res.data;
				if (res.code !== 0) {
					return this.$message.error("获取数据失败");
				}
				this.form.name = res.data.form.name;
				this.form.number = res.data.form.number;
				this.form.remark = res.data.form.remark;
			},
			//表单提交方法
			submit(){
				this.$refs.dialogForm.validate(async (valid) => {
					if (valid) {
						this.isSaveing = true;
						var res = await this.$API.sysRole.createOrEditRole.post(this.form);
						this.isSaveing = false;
						if(res.code == 0){
							this.$emit('success', this.form, this.mode)
							this.visible = false;
							this.$message.success("操作成功")
						}else{
							this.$alert(res.message, "提示", {type: 'error'})
						}
					}
				})
			},
			//表单注入数据
			setData(id){
				console.log(id);
				this.form.id = id;
				// 请求数据
				this.getRoleData(id)
				//可以和上面一样单个注入，也可以像下面一样直接合并进去
				//Object.assign(this.form, data)
			}
		}
	}
</script>

<style>
</style>
