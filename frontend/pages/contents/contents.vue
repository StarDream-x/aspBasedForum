<template>
	<view class="card-list">
		<ContentCard v-for="contents in list" @click="goTo(contents)" :key="contents.id" :title = "contents.title" :author="contents.authorName" :viewCount="contents.viewCount" :imageSrc="contents.imageUrl" />
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	import ContentCard from '~@/components/content-card.vue'
	export default {
		components: {
			ContentCard
		},
		data() {
			return {
				prevRequest: null,
				list:[]			
			}
		},
		methods: {
			goTo(contents){
				getApp().globalData.toDetailContentId=contents.id
				uni.navigateTo({
					url:"/pages/detail/detail",
					success: (res) => {
					}
				})
			},
			getContent(){
				let token = uni.getStorageSync('token');
				if (token) {
					// 获得页面信息
					myRequest({
						url: 'content',
						method: 'GET',
						data: {
							prevRequest: this.prevRequest,
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							this.prevRequest = res.data.requestId
							this.list = [...this.list,...res.data.contents]
						}
					})
				}
				else {
					uni.showToast({
					icon: "error",
					title: "请登录后重试"
					})
				}
			}
		},
		onLoad() {
			this.getContent()
		},
		onReachBottom() {
			this.getContent()
		},
		onPullDownRefresh() {
			this.prevRequest = null
			this.list = []
			this.getContent()
			uni.stopPullDownRefresh()
		}
	}
</script>

<style>
	.card-list {
		display: grid;
		grid-template-columns: 375rpx 375rpx;
		row-gap: 10rpx;
		align-items: center;
		justify-items: center;
	}
</style>
