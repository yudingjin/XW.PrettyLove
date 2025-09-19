// app.js
App({
  onLaunch() {
    // 展示本地存储能力
    const logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)

    // 检查是否已登录
    const storedSessionId = wx.getStorageSync('sessionId')
    const storedOpenId = wx.getStorageSync('openId')
    
    if (storedSessionId && storedOpenId) {
      // 如果有存储的登录信息，直接放入全局变量
      this.globalData.sessionId = storedSessionId
      this.globalData.openId = storedOpenId
      this.globalData.unionId = wx.getStorageSync('unionId') || ''
      console.log('已加载本地存储的登录信息')
      return
    }

    // 未登录则发起登录
    this.login()
  },
  
  // 封装登录方法
  login() {
    wx.login({
      success: (loginRes) => {
        if (!loginRes.code) {
          console.error('登录失败：无法获取code')
          return
        }
        
        // 调用后端登录接口
        wx.request({
          url: `http://192.168.0.26:5045/api/login/${loginRes.code}`,
          method: "GET",
          timeout: 5000, // 添加超时设置
          success: (res) => {
            // 注意：根据后端实际返回格式调整这里的取值
            const data = res.data.data;
            
            if (data.sessionId && data.openId) {
              // 修复原代码中的赋值错误
              this.globalData.sessionId = data.sessionId;
              this.globalData.openId = data.openId;
              this.globalData.unionId = data.unionId || '';
              
              // 存储到本地，防止重启后丢失
              wx.setStorageSync('sessionId', data.sessionId)
              wx.setStorageSync('openId', data.openId)
              if (data.unionId) {
                wx.setStorageSync('unionId', data.unionId)
              }
              
              console.log('登录成功，全局变量：', this.globalData)
            } else {
              console.error('登录失败：返回数据不完整', res.data)
            }
          },
          fail: (err) => {
            console.error('接口请求失败', err)
            wx.showToast({
              title: '登录失败，请重试',
              icon: 'none',
              duration: 2000
            })
          }
        })
      },
      fail: (err) => {
        console.error('wx.login调用失败', err)
      }
    })
  },
  
  globalData: {
    userInfo: null,
    sessionId: '',  // 用于存储会话ID
    openId: '',     // 用于存储用户OpenId
    unionId: ''     // 用于存储用户UnionId
  }
})
    