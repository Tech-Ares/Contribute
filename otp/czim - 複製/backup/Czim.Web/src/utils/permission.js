import tool from '@/utils/tool';

/**
 * 验证permisions
 * @param {验证数据} data 
 * @returns 是否存在
 */
export function permission(data) {
	let permissions = tool.data.get("PERMISSIONS");
	if (!permissions) {
		return false;
	}
	let isHave = permissions.includes(data);
	return isHave;
}

/**
 * 权限验证
 * @param {权限数据} data 
 * @returns 是否存在权限
 */
export function rolePermission(data) {
	let userInfo = tool.data.get("USER_INFO");
	if (!userInfo) {
		return false;
	}
	let role = userInfo.role;
	if (!role) {
		return false;
	}
	let isHave = role.includes(data);
	return isHave;
}
