<template>
	<view class="user-list">
		<UserListItem v-for="user in users" :userId="user.id" :avatarUrl="user.avatarUrl" :username="user.username" :introduction="user.introduction" :following="false" @click="goToUser(user)" />
		<!-- <UserListItem avatarUrl="/static/logo.png" username="用户名" introduction="个人介绍" :following="false" />
		<UserListItem avatarUrl="/static/logo.png" username="用户名" introduction="个人介绍" :following="false" /> -->
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	import UserListItem from '~@/components/user-list-item.vue'
	export default {
		data() {
			return {
				users:[]
			}
		},
		components: {
			UserListItem
		},
		methods: {
			goToUser(user){
				//TODO:前往他人主页
			}
		},
		onLoad(options){
			const userId = getApp().globalData.toUserlistId
			if(getApp().globalData.userToWhitch == 1){
				//TODO:获取用户粉丝列表
				myRequest({
					url: 'user/'+this.userId+"/follower",
					method: 'GET',
					data: {
						id: userId
					}
				}).then((res) => {
					if (res.statusCode == 200){
						this.users = res.data
					}
				})
			}
			else{
				//TODO:获取用户关注列表
				myRequest({
					url: 'user/'+this.userId+"/following",
					method: 'GET',
					data: {
						id: userId
					}
				}).then((res) => {
					if (res.statusCode == 200){
						this.users = res.data
					}
				})
			}
		}
	}
</script>

<style>
	.user-list {
		width: 750rpx;
	}
</style>
