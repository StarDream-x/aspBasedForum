<template>
	<UserInfoPanel ref="userInfoPanel"/>
	<view class="post-list">
		<view class="post-list__head">发布内容</view>
		<view class="post-list__body">
			<ContentListItem v-for="contents in list" :content="contents" />
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
				userId:null,
				lastContentId: null,
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
				let token = uni.getStorageSync('token');
				if(token){
					uni.getStorage({
						key: 'userId',
						success: (res) => {
							this.userId = res.data
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
									if (this.list.length !== 0) {
										this.lastContentId = this.list[this.list.length-1].id
									}
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
			this.getInfo();
		},
		onReady() {
			this.$refs.userInfoPanel.setUserId(this.userId)
		},
		onShow() {
			this.$refs.userInfoPanel.setUserId(this.userId)
		},
		onReachBottom() {
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
					if (this.list.length !== 0) {
						this.lastContentId = this.list[this.list.length-1].id
					}
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
