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
				userId:'',
				source:''
			}
		},
		components: {
			UserListItem
		},
		methods: {
			getList(){
				if(this.source == 'fans'){
					//TODO:获取用户粉丝列表
					
				}
				else{
					//TODO:获取用户关注列表
					
				}
			}
		},
		onLoad: go(options){
			this.event = this.getOpenerEventChannel()
			//接收页面1传来的数据
			this.event.on('id', (res) => {
				this.userId = res[0],
				this.source = res[1]
			})
			getList();
		}
	}
</script>

<style>
	.user-list {
		width: 750rpx;
	}
</style>
