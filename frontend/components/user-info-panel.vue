<template>
	<view class="user-info">
		<image class="avatar" :src="userImg" mode="aspectFill"></image>
		<text class="username">{{username}}</text>
		<view v-if="!this.self" class="follow-buttons" @click="follow">
			<button v-if="this.following" class="follow-button">已关注</button>
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
		data() {
			return {
				userImg: '',
				username: '',
				self: false,
				following: false,
				follownum: 0,
				fans: 0,
				collect: 0
			};
		},
		props: [
		],
		methods: {
			follow() {
				console.log('guanzhu clicked')
				//关注与取关事件
				myRequest({
					url: "me/follow/" + this.userId,
					method: 'PATCH',
					data: {
						following: !this.following
					}
				}).then((res) => {
					if (res.statusCode == 200) {
						this.following = res.data.following
					}
				})
			},
			getFans() {
				getApp().globalData.toUserlistId = this.userId
				getApp().globalData.userToWhitch = 1
				uni.navigateTo({
					url: "/pages/user-list/user-list",
					success: (res) => {
					}
				})
			},
			getFollows() {
				getApp().globalData.toUserlistId = this.userId
				getApp().globalData.userToWhitch = 2
				uni.navigateTo({
					url: "/pages/user-list/user-list",
					success: (res) => {
					}
				})
			},
			getFavorites() {
				getApp().globalData.toContentListId = this.userId
				uni.navigateTo({
					url: "/pages/content-list/content-list",
					success: (res) => {
					}
				})
			},
			setUserId(userId) {
				this.userId = userId
				console.log('user-info-panel init')
				const selfId = uni.getStorageSync('userId')
				this.self = (selfId === userId)
				myRequest({
					url: 'user/'+ userId,
					method: 'GET',
				}).then(res => {
					this.userImg = res.data.avatarUrl
					this.username = res.data.username
					this.following = res.data.following
					this.follownum = res.data.followingCount
					this.fans = res.data.followerCount
					this.collect = res.data.favoriteCount
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
