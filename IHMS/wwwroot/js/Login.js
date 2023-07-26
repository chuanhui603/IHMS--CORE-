// 完成 Login 函式

//使用說明:找到你要用的function 在你想要執行function的網頁貼上
//<script src="~/js/Login.js"></script>
//<script>
//    LoadlocalStorage()     <<< = 看你想用那個function就寫進來
//</script>

async function login(event) {
    event.preventDefault();
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
            body: JSON.stringify({ Account: username, Password: password})
        });

        if (res.ok) {
            const member = await res.json();   

            // 登入成功
            alert(`歡迎來到IHMS健康管理平台，${member.name}！`);
            // 將會員資訊存入 localStorage
            localStorage.setItem('currentMember', JSON.stringify(member));

            location.href = "https://localhost:7127";            
           
        } else {
            // 登入失敗
            alert('帳號或密碼不正確，請重新登入！');
        }
    } catch (error) {
        alert('發生錯誤，請稍後再試！');
        console.error(error);
    }

    window.addEventListener('load', function () {
        checkLoginStatus();
    });
}//登入用這個


//從localStorage讀出資料放進會員修改頁面的欄位中
function LoadlocalStorage() {
    //使用教學 在確定有登入的情況下 localStorage會有資料 然後在你想撈資料的網頁貼下面這個

    //<script src="~/js/Login.js"></script>
    //<script>
    //    LoadlocalStorage()
    //</script>

    //要貼在最下面不然讀不到 可以參考 Views\Members\MemberEdit.cshtml 的作法

    // 從localStorage中取出JSON格式的會員資料字串
    const savedMemberJSON = localStorage.getItem('currentMember');

    // 檢查是否有取得會員資料
    if (savedMemberJSON) {

        try {

            // 將JSON字串轉換為JavaScript物件
            const savedMember = JSON.parse(savedMemberJSON);         
            
            // 在這裡可以使用savedMember物件進行你需要的操作                
            document.getElementById("MemberId").value = savedMember.memberId;
            document.getElementById("Name").value = savedMember.name;
            document.getElementById("Email").value = savedMember.email;
            document.getElementById("Phone").value = savedMember.phone;
            document.getElementById("Account").value = savedMember.account;
            document.getElementById("Password").value = savedMember.password;
            document.getElementById("Birthday").value = savedMember.birthday;
            document.getElementById("Gender").value = savedMember.gender;
            document.getElementById("MaritalStatus").value = savedMember.maritalStatus;
            document.getElementById("Nickname").value = savedMember.nickname;
            document.getElementById("Avatarimage").value = savedMember.avatarimage;
            document.getElementById("Residentialcity").value = savedMember.residentialcity;
            document.getElementById("Permission").value = savedMember.permission;
            document.getElementById("Occupation").value = savedMember.occupation;
            document.getElementById("Diseasedescription").value = savedMember.diseasedescription;
            document.getElementById("Allergydescription").value = savedMember.allergydescription;
            document.getElementById("Logintime").value = savedMember.logintime;           

        } catch (error) {
            // 若轉換失敗或資料有誤，處理錯誤情況
            console.error('無效的會員資料:', error);
        }
    } else {
        // 若沒有取得會員資料，處理未登入情況
        console.log('尚未登入');
    }
} //撈資料是這個


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
}//檢查登入狀態

function logout() {
    // 登出，將會員資訊從 localStorage 中移除
    localStorage.removeItem('currentMember');
    // 重新載入頁面
    location.reload();
} //登出

function LoginPermission(userData) {
    // 將 userData 資料讀取出來
    const permissionLevel = userData.permission;

    // 根據 permissionLevel 做權限控制
    if (permissionLevel === 1) {
        // 管理者可以做的事情
        alert(`歡迎來到IHMS健康管理平台，${userData.name}管理者！`);
        console.log('您是管理者，擁有最高權限！');
    } else if (permissionLevel === 1000) {
        // 老師可以做的事情
        alert(`歡迎來到IHMS健康管理平台，${userData.name}老師！`);
        console.log('您是老師，可以進行特定操作！');
    } else if (permissionLevel === 3000) {
        // 會員可以做的事情
        alert(`歡迎來到IHMS健康管理平台，${userData.name}會員！`);
        console.log('您是會員，可以進行一般操作！');
    } else {
        // 限制遊客可以做的事情
        console.log('您無法執行此操作，請先登入。');
        window.location.href = 'https://localhost:7127/Login/Login';
    }
} //登入權限

