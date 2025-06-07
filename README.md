ReadMe

## 📁 Project Structure

MCCOomputers/
├── MCComputers.invoiceAPI/ # Backend (.NET Core Web API)
└── invoice-frontend/ # Frontend (Angular)
---

## 🛠️ Setup Instructions to Run the Project Locally

### 1. Install Prerequisites

Make sure the following tools are installed on your machine:

- **Visual Studio 2022+** (for backend)
- **SQL Server Management Studio (SSMS)** (for database)
- **.NET SDK 6.0 or later**
- **Node.js and npm**
- **Angular CLI** (`npm install -g @angular/cli`)
- **Visual Studio Code** (for frontend)

---

### 2. Set Up the Backend (invoiceAPI)

1. Open the `MCComputers.invoiceAPI` folder in **Visual Studio**.

2. Open **SSMS** and make sure your SQL Server is running.

3. In the `invoiceAPI/appsettings.json` file, update the connection string to match your local SQL Server instance.

   Example:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=InvoiceDB;Trusted_Connection=True;"
   }


4. Open Package Manager Console in Visual Studio and run the following commands to create and apply the database migration:
    Add-Migration InitialCreate
    Update-Database



5. Run the backend project by pressing F5 or Ctrl+F5. Note the port number it runs on (e.g., https://localhost:5001).


### 6. Set Up the Frontend (invoice-frontend)

    Open the invoiceFront folder in Visual Studio Code.

    Install dependencies by running:
    npm install
    Update the API URLs to match your backend port:

7. Go to src/app/services/ folder.

    Open both invoice.service.ts and product.service.ts.
    Change the URL to match the port of your running backend.
    Example:
    private apiUrl = 'https://localhost:5001/api/invoices'; // or your actual backend URL

8. Run the Angular application:

    ng serve
    Open your browser and visit:http://localhost:4200