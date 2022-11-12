<template>
	<view class="user-info">
		<image class="avatar" :src="userImg" mode="aspectFill"></image>
		<text class="username">{{username}}</text>
		<view v-if="!this.self" class="follow-buttons" @click="follow">
			<button v-if="this.followingState" class="follow-button" :disabled="true">已关注</button>
			<button v-else class="follow-button" type="primary">关注</button>
		</view>
		<view class="data-row">
			<view class="data-item data-item-following" @click="getFollows">
				<text class="data-item__data">{{follownum}}</text>
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
</template>

<script>
	import {
		myRequest
	} from '~@/util/api.js'
	export default {
		name: "user-info-panel",
		props: [
			'userId',
			'username',
			'follownum',
			'fans',
			'collect',
			'userImg',
			'self',
			'following'
		],
		data() {
			return {
				followingState: this.following
			};
		},
		methods: {
			follow() {
				console.log('guanzhu clicked')
				//关注与取关事件
				myRequest({
					url: "me/follow/" + this.userId,
					method: 'PATCH',
					data: {
						following: !this.followingState
					}
				}).then((res) => {
					if (res.statusCode == 200) {
						this.followingState = res.data.following
					}
				})
			},
			getFans() {
				uni.navigateTo({
					url: "/pages/user-list/user-list",
					success: (res) => {
						getApp().globalData.toUserlistId = this.userId
						getApp().globalData.userToWhitch = 1
					}
				})
			},
			getFollows() {
				uni.navigateTo({
					url: "/pages/user-list/user-list",
					success: (res) => {
						getApp().globalData.toUserlistId = this.userId
						getApp().globalData.userToWhitch = 2
					}
				})
			},
			getFavorites() {
				uni.navigateTo({
					url: "/pages/content-list/content-list",
					success: (res) => {
						getApp().globalData.toContentListId = this.userId
					}
				})
			}
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

	.follow-buttons {
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
</style>
