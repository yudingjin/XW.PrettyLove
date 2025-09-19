// pages/login/login.js
// 获取应用实例
const app = getApp();

Page({
  data: {
    loading: false
  },

  onLoad() {
    // 检查是否已登录
    this.checkLoginStatus();
  },

  // 检查登录状态
  checkLoginStatus() {
    const token = wx.getStorageSync('memberInfo');
    if (token && token.MemberId) {
      // 已登录，跳转到首页
      wx.switchTab({
        url: '/pages/index/index'
      });
    }
  },

  // 执行登录流程
  async doLogin() {
    if (this.data.loading) return;
    
    this.setData({ loading: true });
    
    try {

      debugger
      // 1. 检查全局变量中是否有sessionId和openId
      if (!app.globalData.sessionId || !app.globalData.openId) {
        // 如果没有，则重新获取登录凭证
        const { code } = await wx.login();

        debugger
        if (!code) {
          throw new Error('获取登录凭证失败');
        }
        
        // 调用后端接口获取sessionId和openId并更新到全局变量
        const code2SessionRes = await this.request('/api/Login/' + code, 'GET');
        if (!code2SessionRes.SessionId || !code2SessionRes.OpenId) {
          throw new Error('获取会话信息失败');
        }
        
        // 更新全局变量
        app.globalData.sessionId = code2SessionRes.SessionId;
        app.globalData.openId = code2SessionRes.OpenId;
        app.globalData.unionId = code2SessionRes.UnionId || '';
      }

      // 2. 获取用户信息
      const userInfoRes = await this.getUserProfile();
      
      // 3. 从全局变量中获取sessionId和openId，验证用户信息并完成登录
      const loginRes = await this.request('/api/Login', 'POST', {
        SessionId: app.globalData.sessionId,  // 从全局变量获取
        OpenId: app.globalData.openId,        // 从全局变量获取
        RawData: userInfoRes.rawData,
        EncryptedData: userInfoRes.encryptedData,
        Iv: userInfoRes.iv,
        Signature: userInfoRes.signature
      });

      // 4. 登录成功，保存用户信息
      wx.setStorageSync('memberInfo', loginRes);
      
      // 5. 跳转到首页
      wx.showToast({
        title: '登录成功',
        icon: 'success',
        duration: 1500
      });
      
      setTimeout(() => {
        wx.switchTab({
          url: '/pages/index/index'
        });
      }, 1500);

    } catch (error) {
      console.error('登录失败:', error);
      wx.showToast({
        title: error.message || '登录失败',
        icon: 'none',
        duration: 2000
      });
    } finally {
      this.setData({ loading: false });
    }
  },

  // 获取用户信息
  getUserProfile() {
    return new Promise((resolve, reject) => {
      wx.getUserProfile({
        desc: '用于完善会员资料', // 声明获取用户信息的用途
        success: (res) => {
          resolve(res);
        },
        fail: (err) => {
          reject(new Error('获取用户信息失败，请允许授权后重试'));
        }
      });
    });
  },

  // 网络请求封装
  request(url, method = 'GET', data = {}) {
    const baseUrl = 'http://192.168.0.26:5045'; // 替换为实际的后端接口域名
    
    return new Promise((resolve, reject) => {
      wx.request({
        url: baseUrl + url,
        method,
        data,
        header: {
          'content-type': 'application/json'
        },
        success: (res) => {
          if (res.statusCode === 200) {
            resolve(res.data);
          } else {
            reject(new Error(res.data || `请求失败，状态码: ${res.statusCode}`));
          }
        },
        fail: (err) => {
          reject(new Error('网络请求失败，请检查网络连接'));
        }
      });
    });
  }
});
