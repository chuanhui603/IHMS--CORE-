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
            body: JSON.stringify({ Account: username, Password: password })
            
        });

        if (res.ok) {
            const member = await res.json();
            
            // 登入成功

            const memberId = member.memberId;
            
            localStorage.setItem('currentMemberId', memberId);
           
            editMember(memberId);            
            
            alert(`歡迎來到IHMS健康管理平台，${member.name}！`);
      
            // 將會員資訊存入 localStorage
            localStorage.setItem('currentMember', JSON.stringify(member));

            location.href = `https://localhost:7127/Login/Edit/${memberId}`;

            /*location.href = `http://localhost:5174/`;*/
            
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
}


// 假設您在前端使用 jQuery
$(document).ready(function () {
    $('#googleLoginButton').click(function () {
        $.ajax({
            url: '/Login/GoogleLogin', // 請替換成實際的控制器路徑
            type: 'POST',
            data: { /* 傳遞所需的資料 */ },
            success: function (data) {
                if (data.success) {
                    // 如果成功，將使用者資料存入 localStorage
                    localStorage.setItem('memberInfo', JSON.stringify(data.memberInfo));
                    // 之後可以在需要的地方從 localStorage 讀取使用者資料
                    window.location.href = '/MemberDashboard'; // 轉到會員資訊頁面
                } else {
                    // 處理失敗的情況
                    alert('Google 授權失敗');
                }
            },
            error: function () {
                alert('發生錯誤');
            }
        });
    });
});

function editMember(memberId) {
    // 使用 AJAX 將 id 發送給後端的 Edit 方法
    $.ajax({
        type: 'GET',
        url: '/Login/Edit',
        data: { id: memberId },
        success: function (result) {
            // 處裡返回結果
        },
        error: function () {
            // 處裡錯誤
        }        
    });


}

async function MemberEdit(event) {
    event.preventDefault(); // 取消表單預設提交行為

    // 取得要修改的會員資料（假設您有個表單並且收集了要修改的資料）
    const formData = new FormData(event.target);
    const memberId = formData.get("MemberId"); // 假設MemberId是您要修改的會員的ID    
    const newAccount = formData.get("Account");
    const newEmail = formData.get("Email");
    const newName = formData.get("Name");
    const newPassword = formData.get("Password");    
    const newPhone = formData.get("Phone");
    const newBirthday = formData.get("Birthday");
    const newResidentialCity = formData.get("ResidentialCity");
    const newDiseaseDescription = formData.get("DiseaseDescription");
    const newAllergyDescription = formData.get("AllergyDescription");
    // 建立要提交的資料

    const data = {
        memberId: memberId,
        account: newAccount,        
        email: newEmail,        
        name: newName,
        password: newPassword,
        phone: newPhone,
        birthday: newBirthday,
        residentialcity: newResidentialCity,
        diseasedescription: newDiseaseDescription,
        allergydescription: newAllergyDescription,        

    };       

    try {
        const response = await fetch(`/api/[controller]/MemberEdit/${memberId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data), // 將資料轉換為JSON格式
        });

        const result = await response.json();
        // 根據伺服器回傳的結果處理相應的行為
        console.log(result); // 在這裡您可以根據回傳的訊息來顯示成功或失敗的訊息，或是重新載入會員資料等等
    } catch (error) {
        console.error("發生錯誤:", error);
    }
}


// 在Login.js檔案中新增以下程式碼
function MemberEdit() {
    var memberId = $("#MemberId").val(); // 獲取會員ID
    // 使用AJAX或其他方式向後端發送請求獲取會員資料
    $.ajax({
        url: "/Members/MemberEdit", // 替換成後端處理資料的API路徑
        method: "GET",
        data: { id: memberId }, // 將會員ID作為請求參數
        success: function (data) {
            // 將從後端獲取的會員資料填充到表單中
            $("#Account").val(data.Account);
            $("#Email").val(data.Email);
            $("#Name").val(data.Name);
            // 其他欄位類似...
        },
        error: function (error) {
            console.log("載入會員資料失敗", error);
        }
    });
}


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
            document.getElementById("AvatarImage").value = savedMember.avatarImage;
            document.getElementById("Residentialcity").value = savedMember.residentialCity;
            document.getElementById("Permission").value = savedMember.permission;
            document.getElementById("Occupation").value = savedMember.occupation;
            document.getElementById("Diseasedescription").value = savedMember.diseaseDescription;
            document.getElementById("Allergydescription").value = savedMember.allergyDescription;
            document.getElementById("Logintime").value = savedMember.loginTime;         




        } catch (error) {
            // 若轉換失敗或資料有誤，處理錯誤情況
            console.error('無效的會員資料:', error);
        }
    } else {
        // 若沒有取得會員資料，處理未登入情況
        console.log('尚未登入');
    }
} //撈資料是這個
function ImgShow() {//目前用不了 推測可能是vue的問題?
    const savedMemberJSON = localStorage.getItem('currentMember');
    if (savedMemberJSON) {
        try {
             
            // 將JSON字串轉換為JavaScript物件
            const savedMember = JSON.parse(savedMemberJSON);
            console.log(savedMemberJSON) //有抓到
            console.log(savedMember.avatarImage) //這邊會顯示圖檔名稱(1.jpg)         
           
            // 取得圖片元素
            document.getElementById("ImgShow").src = '/images/' + savedMember.avatarImage; //不確定是不是正確寫法 因為測不了
            const imgElement = document.getElementById("ImgShow");
            console.log(imgElement);

            
             


        } catch (error) {
            // 若轉換失敗或資料有誤，處理錯誤情況
            console.error('無效的圖片:', error);
        }
    } else {
        // 若沒有取得會員資料，處理未登入情況
        console.error('尚未登入');
    }
}
 



// 在網頁載入時執行設定
document.addEventListener("DOMContentLoaded", function () {
    setAvatarImageSrc();
});


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
        window.location.href = 'https://localhost:7127/Login/Edit/1';
        alert(`歡迎來到IHMS健康管理平台，${userData.name}會員！`);
        console.log('您是會員，可以進行一般操作！');
    } else {
        // 限制遊客可以做的事情
        console.log('您無法執行此操作，請先登入。');
        window.location.href = 'https://localhost:7127/Login/Login';
    }
} //登入權限

function 管理者DemoLogin() {
    // 填入預設的帳號和密碼
    document.getElementById("Account").value = "Super001";
    document.getElementById("Password").value = "password1";
}
function 老師DemoLogin() {
    // 填入預設的帳號和密碼
    document.getElementById("Account").value = "T001";
    document.getElementById("Password").value = "password2";
}
function 會員DemoLogin() {
    // 填入預設的帳號和密碼
    document.getElementById("Account").value = "M001";
    document.getElementById("Password").value = "password3";
}
function 註冊Demo() {
    // 填入預設的帳號和密碼
    document.getElementById("Name").value = "Demo001";
    document.getElementById("Email").value = "Demo@yahoo.com";
    document.getElementById("Account").value = "Jhon Line";
    document.getElementById("Password").value = "password3error";
}
