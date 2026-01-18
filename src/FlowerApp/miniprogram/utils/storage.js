/**
 * @description 存储数据
 * @param {*} key 本地缓存中指定的key
 * @param {*} value 需要缓存的数据
 */
export const setStorage = (key, value) => {
    // 严格校验键名：必须是非空字符串
    if (typeof key !== 'string' || key.trim() === '') {
        console.error('存储失败：key 必须是非空字符串', '传入的key：', key);
        return;
    }
    const validKey = key.trim();
    try {
        wx.setStorageSync(validKey, value)
    } catch (error) {
        console.log(`存储指定${validKey}数据发生了异常`, error)
    }
}

/**
 * @description 从本地读取指定的key的数据
 * @param {*} key 本地缓存中指定的key
 */
export const getStorage = (key) => {
    if (typeof key !== 'string' || key.trim() === '') {
        console.error('读取失败：key 必须是非空字符串', '传入的key：', key);
        return null;
    }
    const validKey = key.trim();

    try {
        const value = wx.getStorageSync(validKey);
        return value;
    } catch (error) {
        console.error(`读取失败：key=${validKey} 发生异常`, '错误详情：', error);
        return null;
    }
}

/**
 * @description 从本地移除指定key的数据
 * @param {*} key 本地存储中的key
 */
export const removeStorage = (key) => {
    if (typeof key !== 'string' || key.trim() === '') {
        console.error('移除失败：key 必须是非空字符串', '传入的key：', key);
        return;
    }
    const validKey = key.trim();
    try {
        wx.removeStorageSync(validKey);
    } catch (error) {
        console.error(`移除失败：key=${validKey} 发生异常`, '错误详情：', error);
    }
}

/**
 * @description 从本地清空全部的数据
 */
export const clearStorage = () => {
    try {
        wx.clearStorageSync()
    } catch (error) {
        console.error(`清除数据发生异常`, '错误详情：', error);
    }
}

/**
 * @description 从storage中获取指定key的数据 异步
 * @param {*} key 指定的key
 */
export const getStorageAsync = (key) => {
    return new Promise(resolve => {
        wx.getStorage({
            key: key,
            complete: (res) => {
                resolve(res)
            }
        })
    })
}

/**
 * @description 从storage中移除指定key的数据 异步
 * @param {*} key 指定的key
 */
export const removeStorageAsync = (key) => {
    return new Promise(resolve => {
        wx.removeStorage({
            key: key,
            complete: (res) => {
                resolve(res)
            }
        })
    })
}

/**
 * @description 清空storage中的所有数据 异步
 */
export const clearStorageAsync = () => {
    return new Promise(resolve => {
        wx.clearStorage({
            complete: (res) => {
                resolve(res)
            }
        })
    })
}

/**
 * @description 缓存数据
 * @param {*} key 缓存中的key
 * @param {*} data 缓存中key对应的数据
 */
export const setStorageAsync = (key, data) => {
    return new Promise(resolve => {
        wx.setStorage({
            key: key,
            data: data,
            complete(res) {
                resolve(res)
            }
        })
    })
}