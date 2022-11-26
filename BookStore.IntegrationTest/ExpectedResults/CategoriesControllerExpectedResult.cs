using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTest.ExpectedResults
{
    public class CategoriesControllerExpectedResult
    {
        public string GetAllCategories_ExpectedResult = @"{
          'content': [
            {
              'category_id': 1,
              'category_name': 'Sách học ngoại ngữ'
            },
            {
              'category_id': 2,
              'category_name': 'Văn học'
            },
            {
              'category_id': 3,
              'category_name': 'Tâm lý kỹ năng'
            },
            {
              'category_id': 4,
              'category_name': 'Kinh tế'
            },
            {
              'category_id': 5,
              'category_name': 'Tiểu sử - Hồi ký'
            },
            {
              'category_id': 6,
              'category_name': 'Sách tham khảo'
            }
          ],
          'error': null,
          'is_success': true
        }";
    } 
}
