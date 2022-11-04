<template>
	<view class="main">
		<view class="author-info">
			<image class="avatar" src="/static/logo.png" mode="aspectFill"></image>
			<text class="author-name">作者名</text>
		</view>
		<ImageSwiper :imageUrls="this.imageUrls" />
		<view class="text">中文文字Lorem ipsum dolor sit amet consectetur adipisicing elit. Totam nobis hic eligendi ipsum accusantium numquam dolor ad, quam porro ut id. Facere ipsa similique accusantium itaque perspiciatis incidunt quas ab.</view>
		<view class="comments">
			<view class="comments__head">评论</view>
			<view class="comments__body">
				<CommentItem avatarUrl="/static/logo.png" authorName='评论发布者' :liked="true" :likeCount="566" content="评论内容" />
				<CommentItem avatarUrl="/static/logo.png" authorName='评论发布者' :liked="true" :likeCount="566" content="评论内容" />
			</view>
		</view>
	</view>
	<view class="bottom-panel">
		<input class="comment-input" type="text" placeholder="发布评论...">
		<view class="bottom-item">
			<image v-if="this.liked" class="bottom-item__icon" src="~@/static/icon/like_primary.svg" mode="aspectFill"></image>
			<image v-else class="bottom-item__icon" src="~@/static/icon/like.svg" mode="aspectFill"></image>
			<text class="bottom-item__text">114</text>
		</view>
		<view class="bottom-item">
			<image v-if="this.favorited" class="bottom-item__icon" src="~@/static/icon/favorite_primary.svg" mode="aspectFill"></image>
			<image v-else class="bottom-item__icon" src="~@/static/icon/favorite.svg" mode="aspectFill"></image>
			<text class="bottom-item__text">114</text>
		</view>
	</view>
</template>

<script>
	import ImageSwiper from '~@/components/image-swiper.vue'
	import CommentItem from '~@/components/comment-item.vue'
	export default {
		data() {
			return {
				liked: true,
				favorited: false,
				imageUrls: [
					'/static/logo.png',
					'/static/logo.png',
					'/static/logo.png'
				]
			}
		},
		components: {
			ImageSwiper,
			CommentItem
		},
		methods: {
			
		}
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
