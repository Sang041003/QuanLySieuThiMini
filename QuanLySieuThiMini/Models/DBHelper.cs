using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;

namespace QuanLySieuThiMini.Models
{
    public class DBHelper
    {
        ProductDBContext dbContext;
        public DBHelper(ProductDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<IdentityUser> GetIdentityUsers()
        {
            return dbContext.Users.ToList();
        }
        public List<Product> GetProducts()
        {
            return dbContext.Products.ToList();
        }
        public List<ProductType> GetProductTypes()
        {
            return dbContext.ProductTypes.ToList();
        }
        public List<Bill> GetBills()
        {
            return dbContext.Bills.ToList();
        }
        public List<Employee> GetEmployees()
        {
            return dbContext.Employees.ToList();
        }
        public List<Guest> GetGuests()
        {
            return dbContext.Guests.ToList();
        }
        public List<Shelf> GetShelves()
        {
            return dbContext.Shelves.ToList();
        }
        public List<Position> GetPositions()
        {
            return dbContext.Positions.ToList();
        }
        public Product DetailProduct(int id)
        {
            return dbContext.Products.First(x=>x.proID==id);
        }
        public void InsertProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            dbContext.Products.Update(product);
            dbContext.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            Product pro = DetailProduct(id);
            dbContext.Products.Remove(pro);
            dbContext.SaveChanges();
        }

        public ProductType DetailProductType(string id)
        {
            return dbContext.ProductTypes.First(x => x.typeID == id);
        }
        public void InsertProductType(ProductType product)
        {
            dbContext.ProductTypes.Add(product);
            dbContext.SaveChanges();
        }
        public void UpdateProductType(ProductType product)
        {
            dbContext.ProductTypes.Update(product);
            dbContext.SaveChanges();
        }
        public void DeleteProductType(string id)
        {
            ProductType pro = DetailProductType(id);
            dbContext.ProductTypes.Remove(pro);
            dbContext.SaveChanges();
        }

        public Position DetailPosition(string id)
        {
            return dbContext.Positions.First(x => x.posID == id);
        }
        public void InsertPosition(Position pos)
        {
            dbContext.Positions.Add(pos);
            dbContext.SaveChanges();
        }
        public void UpdatePosition(Position pos)
        {
            dbContext.Positions.Update(pos);
            dbContext.SaveChanges();
        }
        public void DeletePosition(string id)
        {
            Position pos = DetailPosition(id);
            dbContext.Positions.Remove(pos);
            dbContext.SaveChanges();
        }

        public Shelf DetailShelf(int id)
        {
            return dbContext.Shelves.First(x => x.shelfID == id);
        }
        public void InsertShelf(Shelf shelf)
        {
            dbContext.Shelves.Add(shelf);
            dbContext.SaveChanges();
        }
        public void UpdateShelf(Shelf shelf)
        {
            dbContext.Shelves.Update(shelf);
            dbContext.SaveChanges();
        }
        public void DeleteShelf(int id)
        {
            Shelf shelf = DetailShelf(id);
            dbContext.Shelves.Remove(shelf);
            dbContext.SaveChanges();
        }

        public Employee DetailEmployee(int id)
        {
            return dbContext.Employees.First(x => x.empID == id);
        }
        public void InsertEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
        }
        public void UpdateEmployee(Employee employee)
        {
            dbContext.Employees.Update(employee);
            dbContext.SaveChanges();
        }
        public void DeleteEmployee(int id)
        {
            Employee emp = DetailEmployee(id);
            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();
        }

        public Guest DetailGuest(string phone)
        {
            return dbContext.Guests.First(x => x.guestPhone == phone);
        }
        public void InsertGuest(Guest guest)
        {
            dbContext.Guests.Add(guest);
            dbContext.SaveChanges();
        }
        public void UpdateGuest(Guest guest)
        {
            dbContext.Guests.Update(guest);
            dbContext.SaveChanges();
        }
        public void DeleteGuest(string phone)
        {
            Guest guest = DetailGuest(phone);
            dbContext.Guests.Remove(guest);
            dbContext.SaveChanges();
        }

        public void InsertBill(Bill bill)
        {
            dbContext.Bills.Add(bill);
            dbContext.SaveChanges();
        }

        public void InsertBillDetail(BillDetail billDetail)
        {
            dbContext.BillDetails.Add(billDetail);
            dbContext.SaveChanges();
            
        }

        public IdentityUser DetailUser(string id)
        {
            return dbContext.Users.First(x => x.Id == id);
        }

        public bool UserModelExists(string id)
        {
            return dbContext.Users.Any(x => x.Id == id);
        }

        public bool EmaillExists(string email)
        {
            return dbContext.Employees.Any(x => x.email == email);
        }


        public List<ProductVM> GetProductVM()
        {
            List<ProductVM> productVMs = new List<ProductVM>();

            var result = dbContext.Products.Join(dbContext.ProductTypes,
                            p => p.typeID,
                            t => t.typeID,
                            (p, t) => new { product = p, type = t }).Join(dbContext.Shelves,
                                                                     p=>p.product.shelfID,
                                                                     s=>s.shelfID,
                                                                     (p,s)=> new {product = p, shelf = s});

            foreach (var item in result)
            {
                productVMs.Add(new ProductVM()
                {
                    proID = item.product.product.proID,
                    proName = item.product.product.proName,
                    typeID = item.product.product.typeID,
                    typeName = item.product.type.typeName,
                    inventory = item.product.product.inventory,
                    cost = item.product.product.cost,
                    shelfID = item.product.product.shelfID,
                    shelfLocation = item.shelf.shelfLocation
                });
            }

            return productVMs;
        }

        public List<ProductVM> GetProductVMByCategory(string typeID)
        {
            List<ProductVM> productVMs = new List<ProductVM>();

            var result = dbContext.Products.Join(dbContext.ProductTypes,
                            p => p.typeID,
                            t => t.typeID,
                            (p, t) => new { product = p, type = t }).Join(dbContext.Shelves,
                                                                     p => p.product.shelfID,
                                                                     s => s.shelfID,
                                                                     (p, s) => new { product = p, shelf = s });

            result = result.Where(result => result.product.type.typeID == typeID);

            foreach (var item in result)
            {
                productVMs.Add(new ProductVM()
                {
                    proID = item.product.product.proID,
                    proName = item.product.product.proName,
                    typeID = item.product.product.typeID,
                    typeName = item.product.type.typeName,
                    inventory = item.product.product.inventory,
                    cost = item.product.product.cost,
                    shelfID = item.product.product.shelfID,
                    shelfLocation = item.shelf.shelfLocation
                });
            }

            return productVMs;
        }

        public List<ProductVM> SearchProductVM(string searchString, string typeID)
        {
            List<ProductVM> productVMs = new List<ProductVM>();

            var result = dbContext.Products.Join(dbContext.ProductTypes,
                            p => p.typeID,
                            t => t.typeID,
                            (p, t) => new { product = p, type = t });

            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();

                result = result.Where(result =>
                    result.product.proName.ToLower().Contains(lowerSearchString)
                    || result.product.proID.ToString().ToLower().Equals(lowerSearchString)
                );
            }
            if( typeID != null )
            {
                result = result.Where(result => result.product.typeID.Equals(typeID));
            }

            foreach (var item in result)
            {
                productVMs.Add(new ProductVM()
                {
                    proID = item.product.proID,
                    proName = item.product.proName,
                    typeID = item.product.typeID,
                    typeName = item.type.typeName,
                    inventory = item.product.inventory,
                    cost = item.product.cost
                });
            }

            return productVMs;
        }

        public List<ProductTypeVM> GetProductTypeVM()
        {
            List <ProductTypeVM> productTypeVMs = new List<ProductTypeVM>();
            List<ProductType> productTypes = dbContext.ProductTypes.ToList();
            foreach(var productType in productTypes)
            {
                productTypeVMs.Add(new ProductTypeVM()
                {
                    typeID = productType.typeID, 
                    typeName = productType.typeName
                });
            }
            return productTypeVMs;
        }

        public List<EmployeeVM> GetEmployeeVM()
        {
            List<EmployeeVM> vm = new List<EmployeeVM>();

            var result = dbContext.Employees.Join(dbContext.Positions,
                                                    e => e.posID,
                                                    p => p.posID,
                                                    (e, p) => new { employee = e, position = p });

            foreach (var item in result)
            {
                vm.Add(new EmployeeVM()
                {
                    empID = item.employee.empID,
                    empName = item.employee.empName,
                    empAddress = item.employee.empAddress,
                    empPhone = item.employee.empPhone,
                    age = item.employee.age,
                    gender = item.employee.gender,
                    posID = item.employee.posID,
                    posName = item.position.posName
                });
            }

            return vm;
        }
        public List<EmployeeVM> SearchEmployeeVM(string searchString, string posID)
        {
            List<EmployeeVM> vm = new List<EmployeeVM>();

            var result = dbContext.Employees.Join(dbContext.Positions,
                                                       e => e.posID,
                                                       p => p.posID,
                                                       (e, p) => new { employee = e, position = p });
    
            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();

                result = result.Where(result =>
                    result.employee.empName.ToLower().Contains(lowerSearchString)
                    || result.employee.empID.ToString().ToLower().Equals(lowerSearchString)
                );
            }

            if (posID != null)
            {
                result = result.Where(result => result.employee.posID.Equals(posID));
            }

            foreach (var item in result)
            {
                vm.Add(new EmployeeVM()
                {
                    empID = item.employee.empID,
                    empName = item.employee.empName,
                    empAddress = item.employee.empAddress,
                    empPhone = item.employee.empPhone,
                    age = item.employee.age,
                    gender = item.employee.gender,
                    posID = item.employee.posID,
                    posName = item.position.posName
                });
            }

            return vm;
        }

        public List<GuestVM> GetGuestVM()
        {
            List<GuestVM> vm = new List<GuestVM>();

            var result = dbContext.Guests.ToList();

            foreach (var item in result)
            {
                vm.Add(new GuestVM()
                {
                    guestPhone = item.guestPhone,
                    guestName = item.guestName
                });
            }

            return vm;
        }

        public List<BillStatistics> GetRevenueByDateRange(string startDate, string endDate)
        {
            DateTime startDateTime, endDateTime;

            if (!DateTime.TryParse(startDate, out startDateTime) || !DateTime.TryParse(endDate, out endDateTime))
            {
                throw new ArgumentException("Invalid date format. Please use a valid date format.");
            }
            endDateTime = endDateTime.AddDays(1);

            var billsInDateRange = dbContext.Bills
                .ToList()
                .Where(b =>
                {
                    if (DateTime.TryParse(b.date, out DateTime billDate))
                    {
                        return billDate >= startDateTime && billDate <= endDateTime;
                    }
                    return false;
                })
                .ToList();

            var revenueByDateRange = billsInDateRange
                .GroupBy(b => b.date)
                .Select(g => new BillStatistics
                {
                    Date = g.Key,
                    TotalPrice = g.Sum(b => b.totalPrice)
                })
                .ToList();

            return revenueByDateRange;
        }

        public List<PositionVM> GetPositionVM()
        {
            List<PositionVM> posVM = new List<PositionVM>();
            List<Position> positions = dbContext.Positions.ToList();
            foreach (var pos in positions)
            {
                posVM.Add(new PositionVM()
                {
                    posID = pos.posID,
                    posName = pos.posName
                });
            }
            return posVM;
        }

        public List<ShelfVM> GetShelfVM()
        {
            List<ShelfVM> shelfVM = new List<ShelfVM>();
            List<Shelf> shelves = dbContext.Shelves.ToList();
            foreach (var shelf in shelves)
            {
                shelfVM.Add(new ShelfVM()
                {
                    shelfID = shelf.shelfID,
                    shelfLocation = shelf.shelfLocation
                });
            }
            return shelfVM;
        }
    }
}
