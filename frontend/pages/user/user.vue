<template>
	<view class="user-info">		
		<image class="avatar" :src="userImg" mode="aspectFill"></image>
		<text class="username">{{username}}</text>
		<view class="data-row">
			<view class="data-item data-item-following" @click="getFollows">
				<text class="data-item__data" >{{follownum}}</text>
				<text class="data-item__name">关注</text>
			</view>
			<view class="data-item data-item-follower" @click="getFans">
				<text class="data-item__data">{{fans}}</text>
				<text class="data-item__name">粉丝</text>
			</view>
			<view class="data-item data-item-favorite" @click="getFavorites">
				<text class="data-item__data">{{collect}}</text>
				<text class="data-item__name">收藏</text>
			</view>
		</view>
	</view>
	<view class="post-list">
		<view class="post-list__head">发布内容</view>
		<view class="post-list__body">
			<ContentListItem v-for="contents in list" :title="contents.title" :author="contents.author" :viewCount="contents.viewCount" :imageSrc="contents.imageUrl" />
<!-- 			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" /> -->
		</view>
	</view>
</template>

<script>	
	import ContentListItem from '~@/components/content-list-item.vue'
	import {myRequest} from '~@/util/api.js'
	import Mock from 'mockjs'
	export default {
		data() {
			return {
				userImg:'',
				userId:null,
				username:null,
				follownum:null,
				fans:null,
				collect:null,
				like:null,
				lastContentId:'',
				list:[
				// 	{
				// 	title:"标题1",
				// 	author:"作者1",
				// 	viewCount:"阅读量1",
				// 	imageUrl:'/static/logo.png'
				// },
				// {
				// 	title:"标题2",
				// 	author:"作2",
				// 	viewCount:"阅读量2",
				// 	imageUrl:'/static/logo.png'
				// },
				]
			}
		},
		components: {
			ContentListItem
		},
		methods: {
			getFans(){
				uni.navigateTo({
					url:"/pages/user-list/user-list",
					success: (res) => {
						res.eventChannel.emit('id',[this.userId,'fans'])
					}
				})
			},
			getFollows(){
				uni.navigateTo({
					url:"/pages/user-list/user-list",
					success: (res) => {
						res.eventChannel.emit('id',[this.userId,'follows'])
					}
				})
			},
			getFavorites(){
				uni.navigateTo({
					url:"/pages/content-list/content-list",
					success: (res) => {
						res.eventChannel.emit('id',this.userId)
					}
				})
			},
			getInfo(){
				let token = uni.getStorageSync('token');
				if(token){
					uni.getStorage({
						key: 'userId',
						success: (res) => {
							this.userId = res.data
							myRequest({
								url: 'user/'+this.userId,//存疑：是不是这么写
								method: 'GET',
								data: {
									id: this.userId
								}
							}).then((res) => {
								if (res.statusCode == 200){
									this.userImg = res.data.avatarUrl
									this.username = res.data.username
									this.follownum = res.data.followingCount
									this.fans = res.data.followerCount
									this.collect = res.data.favoriteCount
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
									uni.setStorageSync('lastContentId',this.list[this.list.size()-1].id)//存疑									
								}
							})
						},
						fail() {
							uni.showToast({
							icon: "error",
							title: "登录信息异常"
							})
						}
					});
				}
				else{
					uni.showToast({
					icon: "error",
					title: "请登录后重试"
					})
				}
			}
		},
		onLoad() {
			Mock.mock(/user\//, {
				id: "userID",
				avatarUrl: "/static/logo.png",
				username: "@cname",
				introduction: "介绍",
				"followingCount|1-1000": 1,
				"followerCount|1-1000": 1,
				"favoriteCount|1-1000": 1
			});
			Mock.mock(/content/, [
				{
					"id|1-100": 1,
					title:"标题1",
					authorId: "fsdgsdfg",
					authorName: "@cname",
					viewCount: 4567,
					imageUrl:'/static/logo.png'
				},
				{
						"id|1-100": 1,
						title:"标题1",
						authorId: "fsdgsdfg",
						authorName: "@cname",
						viewCount: 4567,
						imageUrl:'/static/logo.png'
				},
				{
						"id|1-100": 1,
						title:"标题1",
						authorId: "fsdgsdfg",
						authorName: "@cname",
						viewCount: 4567,
						imageUrl:'/static/logo.png'
				},
				{
					"id|1-100": 1,
					title:"标题1",
					authorId: "fsdgsdfg",
					authorName: "@cname",
					viewCount: 4567,
					imageUrl:'/static/logo.png'
				},
				{
						"id|1-100": 1,
						title:"标题1",
						authorId: "fsdgsdfg",
						authorName: "@cname",
						viewCount: 4567,
						imageUrl:'/static/logo.png'
				},
				{
						"id|1-100": 1,
						title:"标题1",
						authorId: "fsdgsdfg",
						authorName: "@cname",
						viewCount: 4567,
						imageUrl:'/static/logo.png'
				}
			]);
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
						uni.setStorageSync('lastContentId',this.list[this.list.size()-1].id)//存疑									
					}
				})
		}
	}
</script>

<style>
	.user-info {
		width: 750rpx;
		display: flex;
		flex-direction: column;
		align-items: center;
		margin-bottom: 40rpx;
	}

	.avatar {
		width: 250rpx;
		height: 250rpx;
		border-radius: 50%;
		margin: 40rpx;
	}
	
	.username {
		font-weight: bold;
		font-size: 48rpx;
		margin-bottom: 40rpx;
	}
	
	.data-row {
		display: flex;
		width: 550rpx;
		justify-content: space-between;
	}
	
	.data-item {
		display: flex;
		flex-direction: column;
		align-items: center;
	}
	
	.data-item__data {
		font-weight: bold;
		font-size: 48rpx;
		margin-bottom: 16rpx;
	}
		
	.data-item__name {
		color: #999;
		font-size: 36rpx;
	}
	
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
