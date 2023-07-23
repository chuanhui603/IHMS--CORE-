// 完成 Login 函式
async function login() { 
    const username = document.getElementById('Account').value;
    const password = document.getElementById('Password').value;

    // 使用 AJAX 發送登入請求
    const baseAddress = `https://localhost:7127/api/Members/Login`;
    try {
        const res = await fetch(baseAddress, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Account: username, Password: password })
        });

        if (res.ok) {
            const member = await res.json();
            // 登入成功
            alert(`歡迎來到IHMS健康管理平台，${member.name}！`);
            // 將會員資訊存入 localStorage
            localStorage.setItem('currentMember', JSON.stringify(member));
            // 重新載入頁面
            location.reload();
        } else {
            // 登入失敗
            alert('帳號或密碼不正確，請重新登入！');
        }
    } catch (error) {
        alert('發生錯誤，請稍後再試！');
        console.error(error);
    }    


    function checkLoginStatus() {
        // 檢查是否有會員登入
        const currentMember = JSON.parse(localStorage.getItem('currentMember'));
        if (currentMember) {
            // 顯示歡迎詞和登出按鈕
            document.getElementById('memberName').innerText = `歡迎您，${currentMember.name}！`;
            document.getElementById('logoutButton').style.display = 'block';
        } else {
            // 若未登入，顯示登入按鈕
            document.getElementById('loginButton').style.display = 'block';
        }
    }

    function logout() {
        // 登出，將會員資訊從 localStorage 中移除
        localStorage.removeItem('currentMember');
        // 重新載入頁面
        location.reload();
    }

    function LoginPermission(userData) {
        // 將 userData 資料讀取出來
        const permissionLevel = userData.permission;

        // 根據 permissionLevel 做權限控制
        if (permissionLevel === 1) {
            // 管理者可以做的事情


        } else if (permissionLevel === 1000) {
            // 老師可以做的事情


        } else if (permissionLevel === 3000) {
            // 會員可以做的事情

        } else {
            // 限制遊客可以做的事情
            console.log(`您無法執行此操作，請先登入。`);
            window.location.href = 'https://localhost:7127/Login/Login';
        }
    }  

    window.addEventListener('load', function () {
        checkLoginStatus();
    });
}