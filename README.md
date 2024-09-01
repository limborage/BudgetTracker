# BudgetTracker
My asp.net budget tracker

## MYSQL DB Command
docker run -itd -p 3306:3306 -e MYSQL_DATABASE=budget_tracker_db -e MYSQL_USER=dbuser -e MYSQL_PASSWORD=dbpass -e MYSQL_RANDOM_ROOT_PASSWORD=true --name=mysql --rm mysql

INSERT INTO Expenses (Cost, Description, DateCreated) VALUES (10.99, "Netflix", NOW());
INSERT INTO Expenses (Cost, Description, DateCreated) VALUES (22.90, "Groceries", NOW());