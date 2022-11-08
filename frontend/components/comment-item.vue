<template>
	<view class="comment-item">
		<view class="top-info">
			<view class="top-info__left">
				<image class="avatar" :src="this.avatarUrl" @click="getUser"></image>
				<text class="author">{{authorName}}</text>
			</view>
			<view class="top-info__right">
				<view class="like" @click="$emit('changeStatus')">
					<image v-if="this.liked" src="../static/icon/like_primary.svg" mode="aspectFill"></image>
					<image v-else src="../static/icon/like.svg" mode="aspectFill"></image>
				</view>
				<text class="like-count">{{likeCount}}</text>
			</view>
		</view>
		<view class="content">
			<text>{{content}}</text>
		</view>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		name:"comment-item",
		props: [
			'userId',
			'avatarUrl',
			'authorName',
			'liked',
			'likeCount',
			'content'
		],
		data() {
			return {
			};
		},
		methods(){
			getUser(){
				getApp().globalData.toOtherUserId = this.userId
				//TODO：跳转至其他用户页面
				uni.navigateTo({
					url:"/pages/other-user/other-user"
				})
			}
		}
	}
</script>

<style>
	.comment-item {
		border-top: 1px solid lightgrey;
	}
	
	.top-info {
		display: flex;
		align-items: center;
		justify-content: space-between;
	}
	
	.top-info__left {
		display: flex;
		align-items: center;
	}
	
	.top-info__right {
		display: flex;
		align-items: center;
	}
	
	.avatar {
		width: 80rpx;
		height: 80rpx;
		border-radius: 50%;
		margin: 16rpx;
	}
	
	.author {
		width: 460rpx;
		margin-left: 20rpx;
		color: #999;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
	
	.like image {
		width: 40rpx;
		height: 40rpx;
		margin-left: 16rpx;
	}
	
	.like-count {
		margin-left: 16rpx;
		width: 80rpx;
		color: #999;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
	
	.content {
		box-sizing: border-box;
		padding: 40rpx 80rpx;
		font-size: 36rpx;
	}
</style>