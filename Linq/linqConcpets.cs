using System;

namespace LinkConceptsDemo;

class LinkConceptsScenario
{
    class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Age { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

    }

    public class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public double Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}


    public static void Run()
    {
        List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Age = "30", City = "New York", IsActive = true },
            new User { Id = 2, Name = "Bob", Age = "25", City = "Los Angeles", IsActive = false },
            new User { Id = 3, Name = "Charlie", Age = "35", City = "Chicago", IsActive = true },
            new User { Id = 4, Name = "David", Age = "28", City = "Houston", IsActive = false },
            new User { Id = 5, Name = "Eve", Age = "32", City = "Phoenix", IsActive = true }
        };


        List<Payment> payments = new List<Payment>
        {
            new Payment { Id = 1, UserId = 1, Amount = 100.0, Status = "Completed" },
            new Payment { Id = 2, UserId = 2, Amount = 50.0, Status = "Pending" },
            new Payment { Id = 3, UserId = 3, Amount = 75.0, Status = "Completed" },
            new Payment { Id = 4, UserId = 4, Amount = 200.0, Status = "Failed" },
            new Payment { Id = 5, UserId = 5, Amount = 150.0, Status = "Completed" }
        };

        // Example 1: Filter active users
        var activeUsers = users.Where(u => u.IsActive).ToList();

        Console.WriteLine("Active Users:");
        foreach (var user in activeUsers)
        {
            Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}, City: {user.City}");
        }


        // Example 2: Filter inactive users
        var InactiveUsers = users.Where(u => !u.IsActive).ToList();

        Console.WriteLine("Inactive Users:");
        foreach (var user in InactiveUsers)
        {
            Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}, City: {user.City}");
        }


        // Example 3: Find user from Chicago
        var userFromC = users.Where(u => u.City == "Chicago").FirstOrDefault();

        Console.WriteLine("User from Chicago:");
        if (userFromC != null)
        {
            Console.WriteLine($"Id: {userFromC.Id}, Name: {userFromC.Name}, Age: {userFromC.Age}, City: {userFromC.City}");
        }

        var startsWithA = users.Where(u => u.Name.StartsWith("A")).ToList();


        var anyUserFromLA = users.Any(u => u.City == "Los Angeles");
        var allUsersAbove20 = users.All(u => int.Parse(u.Age) > 20);


        var totalActiveUsers = users.Count(u => u.IsActive);

        //return only the names of active users
        var activeUserNames = users.Where(u => u.IsActive)
                                    .Select(u => u.Name)
                                    .ToList();


        var allCompletedPaymentsWithRoundedAmount = payments.Where(p => p.Status == "Completed")
                                                        .Select(p => new
                                                        {
                                                            p.Id,
                                                            p.UserId,
                                                            RoundedAmount = Math.Round(p.Amount)
                                                        })
                                                        .ToList();
        Console.WriteLine("Completed Payments with Rounded Amount:");
        foreach (var payment in allCompletedPaymentsWithRoundedAmount)
        {
            Console.WriteLine($"Payment Id: {payment.Id}, UserId: {payment.UserId}, Rounded Amount: {payment.RoundedAmount}");
        }


        var userPayments = users.Join(payments, u => u.Id, p => p.UserId, (u, p) => new
                                                                {
                                                                    UserName = u.Name,
                                                                    PaymentAmount = p.Amount,
                                                                    PaymentStatus = p.Status
                                                                }).ToList();
        Console.WriteLine("User Payments:");
        foreach (var up in userPayments)
        {
            Console.WriteLine($"User: {up.UserName}, Amount: {up.PaymentAmount}, Status: {up.PaymentStatus}");
        }


        var userPayemntsQ = from u in users
                            join p in payments on u.Id equals p.UserId
                            select new
                            {
                                UserName = u.Name,
                                PaymentAmount = p.Amount,
                                PaymentStatus = p.Status
                            };
        Console.WriteLine("User Payments (Query Syntax):");
        foreach (var up in userPayemntsQ)
        {
            Console.WriteLine($"User: {up.UserName}, Amount: {up.PaymentAmount}, Status: {up.PaymentStatus}");
        }

        //Find all users who made completed payments.
        var userPaymentsCompleted = users
    .Join(
        payments,
        u => u.Id,
        p => p.UserId,
        (u, p) => new
        {
            UserName = u.Name,
            PaymentAmount = p.Amount,
            PaymentStatus = p.Status
        })
    .Where(x => x.PaymentStatus == "Completed")
    .ToList();

        Console.WriteLine("Users with Completed Payments:");
        foreach (var up in userPaymentsCompleted)
        {
            Console.WriteLine($"User: {up.UserName}, Amount: {up.PaymentAmount}, Status: {up.PaymentStatus}");
        }


        var userWithMaxP = users.
        Join(payments,
            u => u.Id,
            p => p.UserId,
            (u , p) => new
            {
                UserName = u.Name,
                PaymentAmount = p.Amount
            })
            .Where(x => x.PaymentAmount == payments.Max(p => p.Amount))
            .ToList();
        Console.WriteLine("User with Maximum Payment:");
        foreach (var up in userWithMaxP)
        {
            Console.WriteLine($"User: {up.UserName}, Amount: {up.PaymentAmount}");
        }




        var totalRev = payments.Where(p => p.Status == "Completed").Sum(p => p.Amount);
        Console.WriteLine($"Total Revenue from Completed Payments: {totalRev}");

        var s = payments.Where(p => p.Status == "Failed").Min(p => p.Amount);
        Console.WriteLine($"Minimum Amount from Failed Payments: {s}");


        var groupedPayments = payments
    .GroupBy(p => p.Status);

        Console.WriteLine("Payments Grouped by Status:");
        foreach (var group in groupedPayments)
        {
            Console.WriteLine($"Status: {group.Key}");
            foreach (var payment in group)
            {
                Console.WriteLine($"  Payment Id: {payment.Id}, UserId: {payment.UserId}, Amount: {payment.Amount}");
            }
        }


        var uByC = users.GroupBy(u => u.City);
        Console.WriteLine("Users Grouped by City:");
        foreach (var group in uByC)
        {
            Console.WriteLine($"City: {group.Key}");
            foreach (var user in group)
            {
                Console.WriteLine($"  User Id: {user.Id}, Name: {user.Name}, Age: {user.Age}, IsActive: {user.IsActive}");
            }

        }


        var CwithMu = users.GroupBy(u => u.City).OrderByDescending(g => g.Count()).FirstOrDefault();
        if (CwithMu != null)
        {
            Console.WriteLine($"City with Most Users: {CwithMu.Key} (User Count: {CwithMu.Count()})");
        }


        var TotalRevenuefromCompleted = users.Join(payments,
        u => u.Id, p => p.UserId,(u, p) => new
        {
            UserName = u.Name,
            PaymentAmount = p.Amount,
            PaymentStatus = p.Status,
            City = u.City
        }).Where(x => x.PaymentStatus == "Completed")
        .GroupBy(x => x.City)
        .Select(g => new
        {
            City = g.Key,
            TotalRevenue = g.Sum(x => x.PaymentAmount)
        })
        .ToList();
        Console.WriteLine("Total Revenue from Completed Payments by City:");
        foreach (var item in TotalRevenuefromCompleted)
        {
            Console.WriteLine($"City: {item.City}, Total Revenue: {item.TotalRevenue}");
        }


        var avgAmountPerUser = users.Join(payments ,
        u => u.Id, p => p.UserId, (u , p) => new
        {
            UserName = u.Name,
            PaymentAmount = p.Amount,
            PaymentStatus = p.Status,
            City = u.City
        }).GroupBy(x => x.UserName)
        .Select(g => new
        {
            UserName = g.Key,
            TotalAmount = g.Average(x => x.PaymentAmount)
        })
        .ToList();
        Console.WriteLine("Average Payment Amount by User:");   
        foreach (var item in avgAmountPerUser)
        {
            Console.WriteLine($"User: {item.UserName}, Average Payment Amount: {item.TotalAmount}");
        }   










    }

}