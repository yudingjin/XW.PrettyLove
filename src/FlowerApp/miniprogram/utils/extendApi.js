/**
 * 封装微信小程序的 showToast 方法
 * @param {Object} options - 提示框配置项
 * @param {string} [options.title='数据加载中...'] - 提示文字
 * @param {string} [options.icon='none'] - 图标类型，可选值：none / success / loading / error
 * @param {number} [options.duration=2000] - 提示延迟时间（ms），loading 类型固定为 0（不自动关闭）
 * @param {boolean} [options.mask=true] - 是否显示透明蒙层防止触摸穿透
 * @returns {void}
 */
const toast = ({
    title = '数据加载中...',
    icon = 'none',
    duration = 2000,
    mask = true
} = {}) => {
    // 1. 参数校验：确保核心参数类型正确
    const validIconTypes = ['none', 'success', 'loading', 'error'];
    const finalIcon = validIconTypes.includes(icon) ? icon : 'none';

    // 2. 兼容微信小程序规则：loading 类型的 toast 不自动关闭（duration 强制设为 0）
    const finalDuration = finalIcon === 'loading' ? 0 : Math.max(Number(duration), 1000); // 最小1000ms，避免过短

    // 3. 确保 title 是字符串且非空
    const finalTitle = typeof title === 'string' && title.trim() ? title.trim() : '操作提示';

    // 4. 确保 mask 是布尔值
    const finalMask = !!mask;

    // 5. 调用微信小程序的 showToast 方法
    wx.showToast({
        title: finalTitle,
        icon: finalIcon,
        duration: finalDuration,
        mask: finalMask
    });
};

/**
 * 封装微信小程序的 showModal 方法，通过 Promise 返回用户操作结果
 * @param {Object} options - 模态框配置项
 * @param {string} [options.title='提示'] - 模态框标题
 * @param {string} [options.content='您确定执行该操作吗？'] - 模态框内容
 * @param {string} [options.confirmColor='#f3514f'] - 确定按钮的颜色
 * @param {string} [options.confirmText='确定'] - 确定按钮文字
 * @param {string} [options.cancelText='取消'] - 取消按钮文字
 * @param {boolean} [options.showCancel=true] - 是否显示取消按钮
 * @returns {Promise<boolean>} 点击确定返回 true，点击取消返回 false
 */
const modal = (options = {}) => {
    // 返回 Promise
    return new Promise((resolve) => {
        // 默认参数（覆盖更全面的小程序 showModal 配置）
        const defaultOption = {
            title: '提示',
            content: '您确定执行该操作吗？',
            confirmColor: '#f3514f',
            confirmText: '确定', // 补充常用默认值
            cancelText: '取消', // 补充常用默认值
            showCancel: true // 补充常用默认值
        };
        // 合并默认参数和用户传入参数（用户参数优先级更高）
        const opts = Object.assign({}, defaultOption, options);
        wx.showModal({
            ...opts,
            // complete 会在所有操作（确定/取消）完成后触发
            complete: (res) => {
                // 核心修复：根据用户操作 resolve 对应的值
                if (res.confirm) {
                    // 点击确定，返回 true
                    resolve(true);
                    console.log('点击了确定');
                } else {
                    // 点击取消/弹框关闭，返回 false（兼容所有非确定操作）
                    resolve(false);
                    console.log('点击了取消');
                }
            }
        });
    });
};

export {
    toast,
    modal
}