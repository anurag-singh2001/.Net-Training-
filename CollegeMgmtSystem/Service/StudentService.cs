using AutoMapper;
using CollegeMgmtSystem.common;
using CollegeMgmtSystem.CosmoDB;
using CollegeMgmtSystem.Entity;
using CollegeMgmtSystem.Interface;
using CollegeMgmtSystem.Models;
using IronPdf;

namespace CollegeMgmtSystem.Service
{
    public class StudentService : IStudentService
    {
        private ICosmoDBService _cosmosDbService;
        private IMapper _mapper;

        public StudentService(ICosmoDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDbService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<StudentModel> AddStudent(StudentModel studentModel)
        {
            if(studentModel.Photo != null && studentModel.Photo.Length > 0)
    {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder); // ensure folder exists

                var fileName = $"{Guid.NewGuid()}_{studentModel.Photo.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await studentModel.Photo.CopyToAsync(stream);
                }

                studentModel.PhotoPath = $"/uploads/{fileName}"; // relative path for web use
            }

            var student = _mapper.Map<Students>(studentModel);
            student.Initialize(true, CommonCredentials.StudentDocumentType, "Anurag", "Anurag Singh");
            var result = await _cosmosDbService.AddStudent(student);
            var dept = await _cosmosDbService.GetDepartmentByUid(student.DeptUid);
            var college = await _cosmosDbService.GetCollegeByUid(dept.ClgUid);

           

            var responseModel = _mapper.Map<StudentModel>(result);
            var path = await GeneratePDF(student.UId);
            if(studentModel.Email!=null)
            {
                string email = studentModel.Email;
                string username = studentModel.Name;
                string subject = "Addmission Completed";
                string message = $"Sucessfully added to college {college.Name} in department {dept.Name}";

                EmailService emailService = new EmailService();
                emailService.SendEmail(subject,email,username, message, path).Wait();
            }
            return responseModel;
        }

        public async Task<List<StudentModel>> GetAllStudents()
        {
            var students = await _cosmosDbService.GetAllStudents();
            var studentModels = new List<StudentModel>();

            foreach (var student in  students)
            {
                var dept = await _cosmosDbService.GetDepartmentByUid(student.DeptUid);
                var clg = await _cosmosDbService.GetCollegeByUid(dept.ClgUid);

                var studentModel = _mapper.Map<StudentModel>(student);
                studentModel.DeptName = dept.Name;
                studentModel.ClgName = clg.Name;

                studentModels.Add(studentModel);
            }

            return studentModels;
        }

        public async Task<List<StudentModel>> GetAllStudentByDeptUid(string Uid)
        {
            var students = await _cosmosDbService.GetAllStudentByDeptUid(Uid);
            var studentModels = new List<StudentModel>();

            foreach (var student in students)
            {
                var dept = await _cosmosDbService.GetDepartmentByUid(student.DeptUid);
                var clg = await _cosmosDbService.GetCollegeByUid(dept.ClgUid);

                var studentModel = _mapper.Map<StudentModel>(student);
                studentModel.DeptName = dept.Name;
                studentModel.ClgName = clg.Name;

                studentModels.Add(studentModel);
            }

            return studentModels;

        }

        public async Task<StudentModel> GetStudentByUid(string UId)
        {
            var students = await _cosmosDbService.GetStudentByUid(UId);
            
            var dept = await _cosmosDbService.GetDepartmentByUid(students.DeptUid);
            var clg = await _cosmosDbService.GetCollegeByUid(dept.ClgUid);

            var studentModel = _mapper.Map<StudentModel>(students);
            studentModel.DeptName = dept.Name;
            studentModel.ClgName = clg.Name;
            

            return studentModel;
        }

        public async Task<string> GeneratePDF(string UId)
        {
            var student = await GetStudentByUid(UId);

            if (student == null)
            {
                return "Student not found";
            }

            
            string photoFileName = Path.GetFileName(student.PhotoPath);
            string photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", photoFileName);

            Console.WriteLine("PHOTO PATH: " + photoPath);


            if(!System.IO.File.Exists(photoPath))
                return "Photo not found.";

            byte[] imageBytes = await File.ReadAllBytesAsync(photoPath);
            string base64Image = Convert.ToBase64String(imageBytes);
            string imageSrc = $"data:image/jpeg;base64,{base64Image}";

            


            string htmlContent = $@"
        <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f7f7f7;
                        padding: 30px;
                    }}
                    .card {{
                        width: 400px;
                        border: 2px solid #333;
                        border-radius: 10px;
                        padding: 20px;
                        background-color: #fff;
                        box-shadow: 0 4px 8px rgba(0,0,0,0.1);
                        margin: auto;
                        text-align: center;
                    }}
                    .photo {{
                        width: 120px;
                        height: 150px;
                        object-fit: cover;
                        border: 1px solid #aaa;
                        border-radius: 5px;
                        margin-bottom: 15px;
                    }}
                    .title {{
                        font-size: 22px;
                        font-weight: bold;
                        margin-bottom: 20px;
                        color: #2c3e50;
                    }}
                    .info {{
                        font-size: 16px;
                        margin: 6px 0;
                        color: #34495e;
                    }}
                </style>
            </head>
            <body>
                <div class='card'>
                    <div class='title'>Student ID Card</div>
                    <img class='photo' src='{imageSrc}' alt='Student Photo' />
                    <div class='info'><strong>UId:</strong> {student.UId}</div>
                    <div class='info'><strong>Name:</strong> {student.Name}</div>
                    <div class='info'><strong>Email:</strong> {student.Email}</div>
                    <div class='info'><strong>Dept:</strong> {student.DeptName}</div>
                    <div class='info'><strong>College:</strong> {student.ClgName}</div>
                </div>
            </body>
        </html>";

            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(htmlContent);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Student-Pdf", $"{student.UId}_card.pdf");

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            pdf.SaveAs(filePath);

            return filePath;

        }
    }
}
