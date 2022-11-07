<template>
	<view class="content-list">
		<ContentListItem v-for="content in contents" :viewCount="content.viewCount" :author="content.authorName" :title="content.title" :imageSrc="content.imageUrl" @click="goToDetail(content)"/>
		<!-- <ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" /> -->
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	import ContentListItem from '~@/components/content-list-item.vue'
	export default {
		data() {
			return {
				contents:[]
			}
		},
		components: {
			ContentListItem
		},
		methods: {
			goToDetail(content){
				uni.navigateTo({
					url:"/pages/detail/detail",
					success: (res) => {
						getApp().globalData.toDetailContentId = content.id
					}
				})
			}
		},
		onLoad(options){
			//TODO:获取用户收藏的文章列表
			const userId = getApp().globalData.toContentListId
			myRequest({
				url: 'user/'+this.userId+"/favorite",
				method: 'GET',
				data: {
					id: userId
				}
			}).then((res) => {
				if (res.statusCode == 200){
					this.contents = res.data
				}
			})
		}
	}
</script>

<style>

</style>
