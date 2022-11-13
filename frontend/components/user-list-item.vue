<template>
	<view class="user-list-item">
		<image class="avatar" :src="this.avatarUrl" mode="aspectFill" @click="toUser"></image>
		<view class="texts">
			<text class="username">{{username}}</text>
			<text class="introduction">{{introduction}}</text>
		</view>
		<view class="follow-buttons" @click="follow">
			<button v-if="this.following" class="follow-button following" :disabled="true">已关注</button>
			<button v-else class="follow-button not-following" type="primary">关注</button>
		</view>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		name: "user-list-item",
		props: [
			'avatarUrl',
			'username',
			'introduction',
			'following',
			'userId'
		],
		data() {
			return {};
		},
		methods: {
			toUser(){
				getApp().globalData.toOtherUserId = this.userId
				//TODO：跳转至其他用户页面
				uni.navigateTo({
					url:"/pages/other-user/other-user"
				})
			},
			follow() {
				//关注与取关事件
				myRequest({
					url: "me/follow/" + this.userId,
					method: 'PATCH',
					data: {
						target_id: this.userId
					}
				}).then((res) => {
					if (res.statusCode == 200) {
						this.following = res.data
					}
				})
			}
		}
	}
</script>

<style>
	.user-list-item {
		border-bottom: 1px solid lightgrey;
		box-sizing: border-box;
		padding: 20rpx 40rpx;
		display: flex;
		align-items: center;
		column-gap: 20rpx;
		justify-content: stretch;
	}

	.avatar {
		width: 140rpx;
		height: 140rpx;
		border-radius: 50%;
	}

	.texts {
		display: flex;
		flex-direction: column;
		justify-content: stretch;
		flex-grow: 1;
	}

	.username {
		font-size: 40rpx;
		margin-bottom: 20rpx;
	}

	.introduction {
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
</style>
