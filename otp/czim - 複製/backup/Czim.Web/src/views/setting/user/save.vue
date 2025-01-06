<template>
	<el-dialog
		:title="titleMap[mode]"
		v-model="visible"
		:width="500"
		destroy-on-close
		@closed="$emit('closed')"
	>
		<el-form
			:model="form"
			:rules="rules"
			ref="dialogForm"
			label-width="100px"
			label-position="left"
			v-loading="loading"
		>
			<el-form-item label="用户名" prop="name">
				<el-input v-model="form.name" placeholder="请输入完整的真实姓名" clearable></el-input>
			</el-form-item>
			<el-form-item label="登录账号" prop="loginName">
				<el-input v-model="form.loginName" placeholder="用于登录系统" clearable></el-input>
			</el-form-item>
			<el-form-item label="登录密码" prop="password">
				<el-input type="password" v-model="form.password" clearable show-password></el-input>
			</el-form-item>
			<el-form-item label="所属角色">
				<el-select v-model="currentRole" placeholder="请选择">
					<el-option v-for="item in roleList" :key="item.value" :label="item.label" :value="item.value"></el-option>
				</el-select>
			</el-form-item>
			<el-form-item label="所在岗位">
				<el-select v-model="currentPost" placeholder="请选择">
					<el-option v-for="item in postList" :key="item.value" :label="item.label" :value="item.value"></el-option>
				</el-select>
			</el-form-item>
			<el-form-item label="手机号" prop="phone">
				<el-input v-model="form.phone" placeholder="请输入手机号" clearable></el-input>
			</el-form-item>
			<el-form-item label="邮箱" prop="email">
				<el-input v-model="form.email" placeholder="请输入邮箱" clearable></el-input>
			</el-form-item>
		</el-form>
		<template #footer>
			<el-button @click="visible = false">取 消</el-button>
			<el-button v-if="mode != 'show'" type="primary" :loading="isSaveing" @click="submit()">保 存</el-button>
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
			loading: false,
			//表单数据
			form: {
				name: "",
				loginName: "",
				password: "",
				phone: "",
				email: ""
			},
			postList: [],
			roleList: [],
			currentRole: '',
			currentPost: '',
			//验证规则
			rules: {
				loginName: [
					{ required: true, message: '请输入登录账号', trigger: 'blur' }
				],
				name: [
					{ required: true, message: '请输入真实姓名', trigger: 'blur' }
				],
				password: [
					{ required: true, message: '请输入登录密码', trigger: 'blur' },
					{ pattern: /(?!(?:[^a-zA-Z]|\D|[a-zA-Z0-9])$).{6,}/, message: '密码格式错误', trigger: 'blur' }
					// {
					// 	validator: (rule, value, callback) => {
					// 		if (this.form.password2 !== '') {
					// 			this.$refs.dialogForm.validateField('password2');
					// 		}
					// 		callback();
					// 	}
					// }
				],
			},
		}
	},
	mounted() {
		// this.getGroup()
	},
	methods: {
		// 根据id查询当前用户数据
		async getUserData(id) {
			this.loading = true
			const res = await this.$API.sysUser.getUserById.get(id)
			console.log(res)
			this.loading = false
			if (res.code !== 0) {
				return this.$message.warning("获取数据失败")
			}
			this.postList = res.data.allPostList
			this.roleList = res.data.allRoleList
			if (this.mode == 'edit') {
				this.currentPost = res.data.postIds[0].postId
				this.currentRole = res.data.roleIds[0].roleId
			}
			this.form = res.data.form
		},
		//显示
		open(mode = 'add') {
			this.mode = mode;
			this.visible = true;
			return this
		},
		//表单提交方法
		submit() {
			this.$refs.dialogForm.validate(async (valid) => {
				if (!this.currentRole.length) {
					this.$message.warning("请选择所属角色")
					return false;
				}
				if (!this.currentPost.length) {
					this.$message.warning("请选择所在岗位")
					return false;
				}
				if (valid) {
					this.isSaveing = true;
					var res = await this.$API.sysUser.saveForm.post({ form: this.form, roleIds: [this.currentRole], postIds: [this.currentPost] });
					this.isSaveing = false;
					if (res.code == 0) {
						// this.$emit('success', this.form, this.mode)
						this.$emit('success')
						this.visible = false;
						this.$message.success("操作成功")
					} else {
						this.$alert(res.message, "提示", { type: 'error' })
					}
				} else {
					return false;
				}
			})
		},
		//表单注入数据
		setData(id) {
			if (this.mode == 'edit') {
				this.getUserData(id)
			} else {
				this.getUserData(id)
			}
			// this.getUserData(id)
			//可以和上面一样单个注入，也可以像下面一样直接合并进去
			//Object.assign(this.form, data)
		}
	}
}
</script>

<style>
</style>
