<template>
	<view class="main">
		<view class="author-info">
			<image class="avatar" :src="detailUser.avatarUrl" mode="aspectFill" @click="toUser"></image>
			<text class="author-name">{{detailUser.username}}</text>
		</view>
		<ImageSwiper :imageUrls="this.imageUrls" />
		<view class="title">{{this.content.title}}</view>
		<view class="text">{{this.content.body}}</view>
		<view class="comments">
			<view class="comments__head">评论</view>
			<view class="comments__body">
				<CommentItem v-for="comment in comments" :userId='comment.user.id' :avatarUrl="comment.user.avatarUrl" :authorName="comment.user.username"
					:liked="comment.liked" :likeCount="comment.likeCount" :content="comment.content"
					@changeStatus='changeStatus(comment)' />
			</view>
		</view>
	</view>
	<view class="bottom-panel">
		<input class="comment-input" type="text" placeholder="发布评论..." @click="goComment">
		<view class="bottom-item" @click="like">
			<image v-if="this.contentInteraction.like" class="bottom-item__icon" src="~@/static/icon/like_primary.svg" mode="aspectFill">
			</image>
			<image v-else class="bottom-item__icon" src="~@/static/icon/like.svg" mode="aspectFill"></image>
			<text class="bottom-item__text">{{this.content.likeCount}}</text>
		</view>
		<view class="bottom-item" @click="favorate">
			<image v-if="this.contentInteraction.favorite" class="bottom-item__icon" src="~@/static/icon/favorite_primary.svg"
				mode="aspectFill"></image>
			<image v-else class="bottom-item__icon" src="~@/static/icon/favorite.svg" mode="aspectFill"></image>
			<text class="bottom-item__text">{{this.content.favoriteCount}}</text>
		</view>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	import ImageSwiper from '~@/components/image-swiper.vue'
	import CommentItem from '~@/components/comment-item.vue'
	export default {
		data() {
			return {
				imageUrls: [
				],
				comments: [],
				content: {
					id:'',
					title:'',
					aurthorId:'',
					viewCount:0,
					likeCount:0,
					favoriteCount:0,
					publishTime:0,
					body:'',
					media:null,
					comments:null
				},
				detailUser: {
					username:'',
					avatarUrl:''
				},
				contentInteraction: {
					like:false,
					favorite:false
				}
			}
		},
		components: {
			ImageSwiper,
			CommentItem
		},
		methods: {
			toUser(){
				getApp().globalData.toOtherUserId = this.detailUser.id
				//TODO：跳转至其他用户页面
				uni.navigateTo({
					url:"/pages/other-user/other-user"
				})
			},
			//用户点赞事件
			like() {
				if (this.contentInteraction.like == false) {
					myRequest({
						url: "content/" + this.content.id + "/interaction",
						method: 'PATCH',
						data: {
							like: true
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							this.contentInteraction = res.data
						};
					})
					this.content.likeCount++
				} else {
					myRequest({
						url: "content/" + this.content.id + "/interaction",
						method: 'PATCH',
						data: {
							like: false
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							this.contentInteraction = res.data
						};
					})
					this.content.likeCount--
				}
			},
			//用户收藏事件
			favorate() {
				if (this.contentInteraction.favorite == false) {
					myRequest({
						url: "content/" + this.content.id + "/interaction",
						method: 'PATCH',
						data: {
							favorite: true
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							this.contentInteraction = res.data
						};
					})
					this.content.favoriteCount++
				} else {
					myRequest({
						url: "content/" + this.content.id + "/interaction",
						method: 'PATCH',
						data: {
							favorite: false
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							this.contentInteraction = res.data
						};
					})
					this.content.favoriteCount--
				}
			},
			//接收评论点赞的事件
			changeStatus(comment) {
				if (comment.liked == true) {
					myRequest({
						url: 'content/' + this.content.id + '/comment/' + comment.id,
						method: 'PATCH',
						data: {
							like: false
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							comment.liked = res.data.like
						};
					})
					comment.likeCount--
				} else {
					myRequest({
						url: 'content/' + this.content.id + '/comment/' + comment.id,
						method: 'PATCH',
						data: {
							like: true
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							comment.liked = res.data.like
						};
					})
					comment.likeCount++
				}
			},
			goComment() {
				uni.navigateTo({
					url: '/pages/new-comment/new-comment',
					success: (res) => {
						getApp().globalData.toCommentId = contents.id
					}
				})
			}
		},
		async onLoad(options) {
			
			const contentsId = getApp().globalData.toDetailContentId
			let res;
			//获得当前用户与该文章的交互情况（有无赞过，有无收藏过）
			res = await myRequest({
				url: "content/" + contentsId + "/interaction",
				method: 'GET'
			});
			if (res.statusCode == 200) {
				this.contentInteraction = res.data
			}
			
			//获取文章详情
			res = await myRequest({
				url: 'content/' + contentsId,
				method: 'GET'
			});
			if (res.statusCode == 200) {
				this.content = res.data
				this.imageUrls = res.data.media.map(x => x.url)
				this.comments = res.data.comments
			}
			
			//获取文章作者信息
			res = await myRequest({
				url: "user/" + this.content.authorId,
				method: 'GET'
			});
			if (res.statusCode === 200) {
				this.detailUser = res.data
			}
		},

	}
</script>

<style>
	page {
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	.main {
		width: 750rpx;
		box-sizing: border-box;
		padding-bottom: 150rpx;
	}

	.author-info {
		width: 750rpx;
		height: 100rpx;
		border-bottom: 1px solid lightgrey;
		padding-left: 60rpx;
		box-sizing: border-box;
		display: flex;
		align-items: center;
	}

	.avatar {
		width: 60rpx;
		height: 60rpx;
		border-radius: 50%;
	}

	.author-name {
		font-weight: bold;
		font-size: 36rpx;
		margin-left: 40rpx;
	}

	.title {
		padding: 40rpx;
		font-size: 40rpx;
		font-weight: bold;
	}

	.text {
		padding: 40rpx;
		font-size: 36rpx;
		box-sizing: border-box;
	}

	.bottom-panel {
		position: fixed;
		bottom: 0;

		width: 750rpx;
		height: 150rpx;
		box-sizing: border-box;
		padding-right: 40rpx;
		z-index: 5;
		background-color: white;
		border-top: 1px solid lightgrey;

		display: flex;
		justify-content: flex-end;
		align-items: center;
	}

	.comment-input {
		border: 1px solid lightgrey;
		margin: 80rpx;
		border-radius: 6px;
		padding: 20rpx;
		width: 600rpx;
		background-color: #eee;
	}

	.bottom-item {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		margin-left: 40rpx;
	}

	.bottom-item__icon {
		width: 60rpx;
		height: 60rpx;
	}

	.bottom-item__text {
		color: #999;
		font-size: 20rpx;
	}

	.comments__head {
		width: 750rpx;
		border-top: 1px solid lightgrey;
		box-sizing: border-box;
		padding: 16rpx;
		font-weight: bold;
	}
</style>
