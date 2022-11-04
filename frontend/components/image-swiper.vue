<template>
	<swiper class="pictures">
		<swiper-item v-for="(imageUrl, index) in this.imageUrls" :key="index">
			<image :src="imageUrl" mode="aspectFit"></image>
		</swiper-item>
	</swiper>
</template>

<script>
	export default {
		name:"image-swiper",
		props: [
			'imageUrls'
		],
		data() {
			return {
			};
		}
	}
</script>

<style>
	.pictures {
		width: 750rpx;
		height: 1000rpx;
	}
	
	image {
		background-color: black;
		width: 100%;
		height: 100%;
		box-sizing: border-box;
	}
</style>