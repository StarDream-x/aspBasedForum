<template>
	<UserInfoPanel :username="this.username" :follownum="this.follownum" 
	:fans="this.fans" :collect="this.collect" :userImg="this.userImg" 
	:self="this.self" :following="this.following" :userId="this.userId"/>
	<view class="post-list">
		<view class="post-list__head">发布内容</view>
		<view class="post-list__body">
			<ContentListItem v-for="content in list" :content="content" />
		</view>
	</view>
</template>

<script>	
	import ContentListItem from '~@/components/content-list-item.vue'
	import UserInfoPanel from '~@/components/user-info-panel.vue'
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				userImg:'',
				userId:null,
				username:null,
				follownum:null,
				following: false,
				fans:null,
				collect:null,
				like:null,
				self: false,
				lastContentId:'',
				list:[
				]
			}
		},
		components: {
			ContentListItem,
			UserInfoPanel
		},
		methods: {
			getInfo(){
				this.userId = getApp().globalData.toOtherUserId
				uni.getStorage({
					key: 'userId',
					success: (res) => {
						this.self = (res.data === this.userId)
					},
					fail: (res) => {
						this.self = false
					}
				})
				myRequest({
					url: 'user/'+this.userId,
					method: 'GET',
				}).then((res) => {
					if (res.statusCode == 200){
						this.userImg = res.data.avatarUrl
						this.username = res.data.username
						this.follownum = res.data.followingCount
						this.fans = res.data.followerCount
						this.collect = res.data.favoriteCount
						this.following = res.data.following
					}
				})
				uni.getStorage({
					key: 'lastContentId',
					success: (res) => {
						this.lastContentId = res.data
					},
					fail: () => {
						this.lastContentId = null
					}
				});
				//获取用户发布的信息
				myRequest({
					url: 'content',
					method: 'GET',
					data: {
						userId:this.userId,
						lastContentId: this.lastContentId
					}
				}).then((res) => {
					if (res.statusCode == 200) {
						this.list = [...this.list,...res.data]
						uni.setStorageSync('lastContentId',this.list[this.list.length-1].id)//存疑									
					}
				})
			}
		},
		onLoad() {
			this.getInfo();
		},
		onReachBottom() {
			uni.getStorage({
					key: 'lastContentId',
					success: (res) => {
						this.lastContentId = res.data
					},
					fail: () => {
						this.lastContentId = null
					}
				});
				//获取用户发布的信息
				myRequest({
					url: 'content',
					method: 'GET',
					data: {
						userId:this.userId,
						lastContentId: this.lastContentId
					}
				}).then((res) => {
					if (res.statusCode == 200) {
						this.list = [...this.list,...res.data]
						uni.setStorageSync('lastContentId',this.list[this.list.length-1].id)									
					}
				})
		}
	}
</script>

<style>
	.post-list {
		width: 750rpx;
	}
	
	.post-list__head {
		border-top: 1px solid lightgrey;
		border-bottom: 1px solid lightgrey;
		padding: 16rpx;
		
		font-size: 36rpx;
		color: #999;
		box-sizing: border-box;
		width: 750rpx;
	}
	
	.post-list__body {
		width: 750rpx;
	}
</style>
