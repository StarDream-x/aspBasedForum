<template>
	<view class="user-list">
		<UserListItem v-for="u in users" :userId="u.id" :avatarUrl="u.avatarUrl" :username="u.username" :introduction="u.introduction" :following="u.following" />
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	import UserListItem from '~@/components/user-list-item.vue'
	export default {
		data() {
			return {
				users: [
				]
			}
		},
		components: {
			UserListItem
		},
		methods: {
			goToUser(user){
				getApp().globalData.toOtherUserId = user.id
				//TODO:前往他人主页
				uni.navigateTo({
					url:"/pages/other-user/other-user"
				})
			}
		},
		onLoad(options){
			const userId = getApp().globalData.toUserlistId
			if(getApp().globalData.userToWhitch == 1){
				//TODO:获取用户粉丝列表
				myRequest({
					url: 'user/'+userId+"/follower",
					method: 'GET',
				}).then((res) => {
					if (res.statusCode == 200){
						this.users = res.data
					}
				})
			}
			else{
				//TODO:获取用户关注列表
				myRequest({
					url: 'user/'+userId+"/following",
					method: 'GET',
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
