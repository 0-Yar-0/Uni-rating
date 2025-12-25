### Setup
```bash
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    dotnet add package System.IdentityModel.Tokens.Jwt
    dotnet add package BCrypt.Net-Next

    psql -U postgres -c "CREATE DATABASE University_rating_db;"
    psql -U postgres -c "CREATE USER University_rating_user WITH PASSWORD 'Best_password';"
    psql -U postgres -c "GRANT ALL PRIVILEGES ON DATABASE University_rating_db TO University_rating_user;"

    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
    dotnet ef migrations add Initial
    dotnet ef database update
```


### Data structure
```json
{
    "iteration": {
        "className": {
            "year": {
                "B11": {
                    "ENa": 0,
                    "ENb": 0,
                    "ENc": 0,
                    "Eb": 0,
                    "Ec": 0,
                    "result": 0
                },
                "B12": {
                    "beta121": 0,
                    "beta122": 0,
                    "result": 0
                },
                "B13": {
                    "beta131": 0,
                    "beta132": 0,
                    "result": 0
                },
                "B21": {
                    "beta211": 0,
                    "beta212": 0,
                    "result": 0
                }
            }
        },
        "names": {
            "classes": {
                "A": {
                    "name": "string"
                },
                "B": {
                    "name": "string",
                    "B11": "string",
                    "B12": "string",
                    "B13": "string",
                    "B21": "string"
                },
                "C": {
                    "name": "string"
                }
            }
        }
    }
}
```