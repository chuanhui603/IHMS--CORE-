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
            console.log('這裡是A');
           
            // 從localStorage中取出JSON格式的會員資料字串
            const savedMemberJSON = localStorage.getItem('currentMember');
            console.log(savedMemberJSON);
            // 檢查是否有取得會員資料
            if (savedMemberJSON) {
                console.log('這裡是A');
                try {
                    // 將JSON字串轉換為JavaScript物件
                    const savedMember = JSON.parse(savedMemberJSON);
                    console.log(savedMemberJSON);

                    // 在這裡可以使用savedMember物件進行你需要的操作
                    document.getElementById("name").value = "test";                    
                    document.getElementById("email").value = savedMember.email;
                    document.getElementById("phone").value = savedMember.phone;
                    document.getElementById("account").value = savedMember.account;
                    document.getElementById("password").value = savedMember.password;
                    document.getElementById("birthday").value = savedMember.birthday;
                    document.getElementById("gender").value = savedMember.gender;
                    document.getElementById("maritalStatus").value = savedMember.maritalStatus;
                    document.getElementById("nickname").value = savedMember.nickname;
                    document.getElementById("avatarimage").value = savedMember.avatarimage;
                    document.getElementById("residentialcity").value = savedMember.residentialcity;
                    document.getElementById("permission").value = savedMember.permission;
                    document.getElementById("occupation").value = savedMember.occupation;
                    document.getElementById("diseasedescription").value = savedMember.diseasedescription;
                    document.getElementById("allergydescription").value = savedMember.allergydescription;
                    document.getElementById("logintime").value = savedMember.logintime;                  

                    // 當按下"更新資料"按鈕時，觸發更新會員資料的功能
                    const updateButton = document.getElementById("updateButton");
                    updateButton.addEventListener("click", () => {
                        // 假設你有一個函數來處理會員資料的更新
                        // 這裡示範一個名為updateMemberData的函數
                        updateMemberData(savedMember);
                    });                 
                } catch (error) {
                    // 若轉換失敗或資料有誤，處理錯誤情況
                    console.error('無效的會員資料:', error);
                }
            } else {
                // 若沒有取得會員資料，處理未登入情況
                console.log('尚未登入');
            }
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


    //function checkLoginStatus() {
    //    // 檢查是否有會員登入
    //    const currentMember = JSON.parse(localStorage.getItem('currentMember'));
    //    if (currentMember) {
    //        // 顯示歡迎詞和登出按鈕
    //        document.getElementById('memberName').innerText = `歡迎您，${currentMember.name}！`;
    //        document.getElementById('logoutButton').style.display = 'block';
    //    } else {
    //        // 若未登入，顯示登入按鈕
    //        document.getElementById('loginButton').style.display = 'block';
    //    }
    //}

    //function logout() {
    //    // 登出，將會員資訊從 localStorage 中移除
    //    localStorage.removeItem('currentMember');
    //    // 重新載入頁面
    //    location.reload();
    //}

    //function LoginPermission(userData) {
    //    // 將 userData 資料讀取出來
    //    const permissionLevel = userData.permission;

    //    // 根據 permissionLevel 做權限控制
    //    if (permissionLevel === 1) {
    //        // 管理者可以做的事情


    //    } else if (permissionLevel === 1000) {
    //        // 老師可以做的事情


    //    } else if (permissionLevel === 3000) {
    //        // 會員可以做的事情

    //    } else {
    //        // 限制遊客可以做的事情
    //        console.log(`您無法執行此操作，請先登入。`);
    //        window.location.href = 'https://localhost:7127/Login/Login';
    //    }
    //}   

    //window.addEventListener('load', function () {
    //    checkLoginStatus();
    //});
}
function updateMemberData(member) {
    // 獲取使用者輸入的新資料
    const newName = document.getElementById("name").value;
    const newEmail = document.getElementById("email").value;
    const newPhone = document.getElementById("phone").value;
    // 獲取其他欄位的新資料...

    // 更新會員資料物件的屬性
    member.name = newName;
    member.email = newEmail;
    member.phone = newPhone;
    // 更新其他欄位...

    // 將更新後的會員資料保存回localStorage
    localStorage.setItem("currentMember", JSON.stringify(member));

    // 進行其他的資料更新或後端送出的動作...

    // 提示使用者更新成功
    alert("資料更新成功！");
}