# Student Management System

### **Student Management System Documentation**

#### **Overview**
The **Student Management System** is a web-based application developed using **ASP.NET Core MVC** and **Entity Framework Core**. It provides a platform to manage students, courses, and enrollments efficiently, with features to ensure simplicity, scalability, and user-friendly interactions. This system is designed to streamline administrative tasks for educational institutions.

---

### **Features**

#### **1. Student Management**
- **View Students:** Display a comprehensive list of all students, with filtering options.
- **Add Students:** Add new students to the system with essential details like name and status.
- **Edit Details:** Update student information as needed.
- **Soft Delete:** Deactivate students without permanently deleting their records.
- **Reactivation:** Enable deactivated students when required.

#### **2. Course Management**
- **View Courses:** List all available courses with details such as title, credits, and description.
- **Add Courses:** Create new courses with relevant details.
- **Edit Courses:** Update course information, including title, description, and credits.
- **Delete Courses:** Remove courses from the system when no longer needed.
- **Enrollment Stats:** Identify the most and least popular courses based on enrollments.

#### **3. Enrollment Management**
- **View Enrollments:** Display all enrollments, showing student names, course titles, grades, and dates.
- **Add Enrollments:** Register students for specific courses.
- **Edit Enrollments:** Update enrollment details, such as grades or associated courses.
- **Grade Management:** Assign grades to students directly through the enrollment view.
- **Soft Delete:** Temporarily remove enrollments with an option to restore them.

#### **4. Dashboard and Reporting**
- **Admin Dashboard:** Displays aggregated statistics:
  - Total students, active and inactive counts.
  - Total courses and the most/least popular courses based on enrollment numbers.
  - Total enrollments, including graded and non-graded enrollments.
- **Interactive Tables:** Vertically scrollable tables with fixed headers for easier navigation of large datasets.

---

### **Technical Details**

#### **Frontend**
- **Framework:** Bootstrap for responsive design.
- **UI Enhancements:** Tables with vertical scrolling and fixed headers to improve data visibility.

#### **Backend**
- **Framework:** ASP.NET Core MVC for application logic and EF Core for database interaction.
- **Data Operations:** Efficient CRUD functionality for students, courses, and enrollments.

#### **Database**
- **Structure:**
  - Students Table
  - Courses Table
  - Enrollments Table
- **Dummy Data:** Seeded for testing and demonstration purposes.
- **Optimizations:** Queries optimized for large datasets with eager loading where necessary.

#### **Performance Optimization**
- Heavy queries, such as those involving enrollment statistics, are optimized to avoid delays.
- Pagination implemented for handling large datasets in student and course tables.

---

### **Future Enhancements**
1. **Role-Based Access Control (RBAC):**
   - Implement user roles such as admin, instructor, and student to restrict access to specific features.

2. **Search and Filter Options:**
   - Add a global search bar for courses, students, and enrollments with real-time filtering.

3. **Export Reports:**
   - Enable administrators to generate PDF or Excel reports for key statistics.

4. **Notifications:**
   - Add email or dashboard notifications for new enrollments, grades, or updates.

5. **Cloud Integration:**
   - Migrate the database or application hosting to a cloud platform for better scalability and reliability.

---

### **How to Run the Application**
1. Clone the repository to your local machine.
2. Ensure you have **.NET 7** SDK and a SQL Server instance installed.
3. Update the `appsettings.json` file with your database connection string.
4. Run the following commands in the terminal:
   - `dotnet restore` to install dependencies.
   - `dotnet ef database update` to apply migrations and seed the database.
   - `dotnet run` to start the application.
5. Access the application in your browser at `https://localhost:{port}`.

---

### **Acknowledgments**
This system was developed to streamline the management of educational data, providing a simple yet effective solution for administrators. The collaborative effort focused on delivering a functional, scalable, and user-friendly application.

