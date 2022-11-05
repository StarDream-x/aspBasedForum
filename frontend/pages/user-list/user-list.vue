<template>
	<view class="user-list">
		<UserListItem avatarUrl="/static/logo.png" username="用户名" introduction="个人介绍" :following="false" />
		<UserListItem avatarUrl="/static/logo.png" username="用户名" introduction="个人介绍" :following="false" />
		<UserListItem avatarUrl="/static/logo.png" username="用户名" introduction="个人介绍" :following="false" />
	</view>
</template>

<script>
	import UserListItem from '~@/components/user-list-item.vue'
	export default {
		data() {
			return {
				
			}
		},
		components: {
			UserListItem
		},
		methods: {
			
		}
	}
</script>

<style>
	.user-list {
		width: 750rpx;
	}
</style>
