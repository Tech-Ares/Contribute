<template>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" icon="el-icon-plus" @click="add"></el-button>
				<el-button
					type="danger"
					plain
					icon="el-icon-delete"
					:disabled="selection.length == 0"
					@click="batch_del"
				></el-button>
				<!-- <el-button type="primary" plain :disabled="selection.length != 1" @click="permission">权限设置</el-button> -->
			</div>
			<div class="right-panel">
				<div class="right-panel-search">
					<el-input v-model="searchList.name" placeholder="角色名称" clearable></el-input>
					<el-button type="primary" icon="el-icon-search" @click="searchRole"></el-button>
					<el-button
						type="info"
						plain
						icon="el-icon-refresh"
						@click="resetSearch"
						:disabled="searchList.name.length < 1"
					></el-button>
				</div>
			</div>
		</el-header>
		<el-main class="nopadding">
			<kxTable
				:data="tableData"
				ref="kxTable"
				rowKey="id"
				:total="total"
				:loading="loading"
				:stripe="true"
				@handleCurrentChange="handleCurrentChange"
				@handleSizeChange="handleSizeChange"
				@refresh="refresh"
				@selection-change="selectionChange"
			>
				<el-table-column type="selection" width="50"></el-table-column>
				<el-table-column type="index" label="#" width="50" />
				<el-table-column prop="name" label="角色名称" width="250" />
				<el-table-column prop="number" label="排序" width="150" />
				<el-table-column prop="remark" label="说明" />
				<el-table-column fixed="right" label="操作" width="140">
					<template #default="scope">
						<el-button type="text" size="small" @click="table_edit(scope.row, scope.$index)">编辑</el-button>
						<el-divider direction="vertical"></el-divider>
						<el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
							<template #reference>
								<el-button type="text" size="small" :style="{ color: '#f56c6c' }">删除</el-button>
							</template>
						</el-popconfirm>
					</template>
				</el-table-column>
			</kxTable>
			<!-- <div class="kxTable-table" v-loading="loading">
				<el-table
					:data="tableData"
					ref="kxTable"
					row-key="id"
					style="width: 100%"
					:height="height == 'auto' ? null : '100%'"
					stripe
					@selection-change="selectionChange"
				>
					<el-table-column type="selection" width="50"></el-table-column>
					<el-table-column type="index" label="#" width="50" />
					<el-table-column prop="name" label="角色名称" width="250" />
					<el-table-column prop="number" label="排序" width="150" />
					<el-table-column prop="remark" label="说明" />
					<el-table-column fixed="right" label="操作" width="140">
						<template #default="scope">
							<el-button type="text" size="small" @click="table_edit(scope.row, scope.$index)">编辑</el-button>
							<el-divider direction="vertical"></el-divider>
							<el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
								<template #reference>
									<el-button type="text" size="small" :style="{ color: '#f56c6c' }">删除</el-button>
								</template>
							</el-popconfirm>
						</template>
					</el-table-column>
					<template #empty>
						<el-empty :description="emptyText" :image-size="100"></el-empty>
					</template>
				</el-table>
			</div>
			<div class="kxTable-pagination-box">
				<el-pagination
					background
					:small="true"
					layout="total, sizes, prev, pager, next, jumper"
					:total="total"
					:page-sizes="[10, 20, 50, 100]"
					v-model:currentPage="currentPage"
					@current-change="handleCurrentChange"
					@size-change="handleSizeChange"
				></el-pagination>
				<div class="kxTable-do">
					<el-button @click="refresh" icon="el-icon-refresh" circle style="margin-left:15px"></el-button>
				</div>
			</div>-->
		</el-main>
	</el-container>

	<save-dialog
		v-if="dialog.save"
		ref="saveDialog"
		@success="handleSaveSuccess"
		@closed="dialog.save = false"
	></save-dialog>

	<permission-dialog
		v-if="dialog.permission"
		ref="permissionDialog"
		@closed="dialog.permission = false"
	></permission-dialog>
</template>

<script>
import saveDialog from './save'
import permissionDialog from './permission'

export default {
	name: 'role',
	components: {
		saveDialog,
		permissionDialog
	},
	data() {
		return {
			dialog: {
				save: false,
				permission: false
			},
			tableData: [],
			searchList: {
				name: ''
			},
			total: 0,
			pageSize: 10,
			currentPage: 1,
			selection: [],
			height: '100%',
			search: {
				keyword: null
			},
			loading: false,
			emptyText: "暂无数据"
		}
	},
	mounted() {
		this.getData();
	},
	methods: {
		async getData() {
			this.loading = true;
			const res = await this.$API.sysRole.getSysRole.post({ page: this.currentPage, size: this.pageSize, name: this.searchList.name })
			console.log(res);
			this.loading = false;
			if (res.code !== 0) {
				return this.$message.error("获取数据失败");
			}
			this.tableData = res.data.rows;
			this.pageSize = res.data.pageSize
			this.total = res.data.total
			this.currentPage = res.data.page
		},
		//添加
		add() {
			this.dialog.save = true
			this.$nextTick(() => {
				this.$refs.saveDialog.open()
			})
		},
		//编辑
		table_edit(row) {
			this.dialog.save = true
			this.$nextTick(() => {
				this.$refs.saveDialog.open('edit').setData(row.id)
			})
		},
		//权限设置
		permission() {
			this.dialog.permission = true
			this.$nextTick(() => {
				this.$refs.permissionDialog.open()
			})
		},
		//删除角色
		async table_del(row) {
			const res = await this.$API.sysRole.deleteRole.post([row.id]);
			if (res.code == 0) {
				this.getData();
				this.$message.success("删除成功")
			} else {
				this.$alert(res.message, "提示", { type: 'error' })
			}
		},
		// 翻页控制
		handleCurrentChange(val) {
			this.currentPage = val;
			this.getData();
		},
		// 每页条数控制
		handleSizeChange(val) {
			this.pageSize = val;
			this.currentPage = 1;
			this.getData();
		},
		// 刷新表格
		refresh() {
			// this.$refs.kxTable.clearSelection();
			this.getData();
		},
		//批量删除
		async batch_del() {
			this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？如果删除项中含有子集将会被一并删除`, '提示', {
				type: 'warning'
			}).then(async () => {
				this.loading = true;
				const res = await this.$API.sysRole.deleteRole.post(this.selection.map(item => item.id));
				if (res.code !== 0) {
					Promise.reject(res.message);
				}
				this.getData();
				this.$message.success("操作成功")
			}).catch((err) => {
				console.log(err);
			})
		},
		//表格选择后回调事件
		selectionChange(selection) {
			console.log(selection);
			this.selection = selection;
		},
		// 搜索
		searchRole() {
			this.getData();
		},
		// 重置搜索
		resetSearch() {
			for (let key in this.searchList) {
				console.log(key);
				this.searchList[key] = '';
			}
			this.getData();
		},
		//根据ID获取树结构
		filterTree(id) {
			var target = null;
			function filter(tree) {
				tree.forEach(item => {
					if (item.id == id) {
						target = item
					}
					if (item.children) {
						filter(item.children)
					}
				})
			}
			filter(this.$refs.table.tableData)
			return target
		},
		//本地更新数据
		handleSaveSuccess(data, mode) {
			if (mode == 'add' || mode == 'edit') {
				this.getData();
			}
		}
	}
}
</script>

<style lang="scss" scoped>
.kxTable {
	height: 100%;
}
:deep(.kxTable-table) {
	height: calc(100% - 50px);
}
:deep(.kxTable-pagination-box) {
	height: 50px;
	display: flex;
	align-items: center;
	justify-content: space-between;
	padding: 0 15px;
}
</style>
