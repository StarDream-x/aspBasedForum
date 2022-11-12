<template>
	<view class="content-list">
		<ContentListItem v-for="content in contents" :content="content"/>
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
		},
		onLoad(options){
			//TODO:获取用户收藏的文章列表
			const userId = getApp().globalData.toContentListId
			myRequest({
				url: 'user/'+this.userId+"/favorite",
				method: 'GET',
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
