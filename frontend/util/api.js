const BASE_URL = 'http://localhost:3000/'

export const myRequest = (options) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: BASE_URL + options.url,
			method: options.method || 'GET',
			data: options.data || {},
			header:{
				Authorization:'token='+ uni.getStorageSync('token') || ''//此处是否需要一起封装起来？
			},
			
			success: (res) => {
				if (res.statusCode >= 400 && res.statusCode < 500) {
					return uni.showToast({
						icon: "error",
						title: res.data.errorMsg
					})
				}
				resolve(res)
			},
			fail: (err) => {
				uni.showToast({
					icon: "error",
					title: '请求接口失败'
				})
				reject(err)
			}
		})
	})
}
