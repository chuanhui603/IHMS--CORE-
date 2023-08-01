<script src="https://apis.google.com/js/platform.js" async defer></script>

function GoogleLogin(googleUser) {
    // 取得使用者的 Google 資料
    var profile = googleUser.getBasicProfile();
    
    // 將 Google 資料傳送到後端處理
    var data = {
      email: profile.getEmail(),
      givenName: profile.getGivenName(),
      familyName: profile.getFamilyName(),
      pictureUrl: profile.getImageUrl(),
      gender: profile.getGender(), // 性別
      birthDate: profile.getBirthdate(), // 生日
    };
    
    // 將資料送到後端處理
    $.ajax({
      type: 'POST',
      url: '/api/Login/GoogleLoginCallback',
      contentType: 'application/json',
      data: JSON.stringify(data),
      success: function(response) {
        // 處理回傳的結果
        console.log(response);
      },
      error: function() {
        // 處理錯誤
        console.error('Error calling API.');
      }
    });
}

