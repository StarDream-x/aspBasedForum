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
				prevRequest:'',
				list:[]			
			}
		},
		methods: {
			goTo(contents){
				uni.navigateTo({
					url:"/pages/detail/detail",
					success: (res) => {
						getApp().globalData.toDetailContentId=contents.id
					}
				})
			},
			getContent(){
				let token = uni.getStorageSync('token');
				if (token) {
					//尝试取上一次请求的id
					uni.getStorage({
						key: 'prevRequest',
						success: (res) => {
							this.prevRequest = res.data
						},
						fail: () => {
							this.prevRequest = null
						}
					});
					// 获得页面信息
					myRequest({
						url: 'content',
						method: 'GET',
						data: {
							prevRequest: this.prevRequest,
						}
					}).then((res) => {
						if (res.statusCode == 200) {
							try {
								uni.setStorageSync('prevRequest', res.data.requestId);
							} catch (e) {
								uni.showToast({
									icon: "none",
									title: "存储prevRequest失败"
								})
							}
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
