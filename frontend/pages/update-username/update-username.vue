<template>
	<input type="text" class="username" v-model="this.username">
	<button type="primary" class="submit-button">完成修改</button>
</template>

<script>
	export default {
		data() {
			return {
				username: ''
			}
		},
		methods: {
			
		}
	}
</script>

<style>
	.username {
		padding : 40rpx;
		border-bottom: 1px solid lightgrey;
	}
	
	.submit-button {
		margin: 40rpx 150rpx;
	}
</style>
