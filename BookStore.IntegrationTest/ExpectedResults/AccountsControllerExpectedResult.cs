using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class AccountsControllerExpectedResult
    {
        public string LoginWithUsernameAndPassword_WrongUsernameAndPassword_ExpectedResult = @"{
          'content': null,
          'error': {
            'code': 404,
            'type': 'Not Found',
            'message': 'Sai thông tin tài khoản hoặc mật khẩu.'
          },
          'is_success': false
        }";

        public string CreateAccount_DuplicateUsername_ExpectedResult = @"{
          'content': null,
          'error': {
            'code': 400,
            'type': 'Bad Request',
            'message': 'Tài khoản đã tồn tại, vui lòng chọn lại tài khoản khác.'
          },
          'is_success': false
        }";
    }
}
