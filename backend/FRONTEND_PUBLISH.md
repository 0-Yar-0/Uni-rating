# Automatic frontend build during `dotnet publish`

The backend project is configured to automatically build the frontend during `dotnet publish`.

What happens:
- The MSBuild `BuildFrontend` target runs before publish if a `frontend/package.json` is present.
- It executes `npm ci` and then `npm run build` in the `frontend` folder.
- The frontend build output is written to `backend/wwwroot` (see `frontend/vite.config.js`). The publish output will therefore include the static files under `wwwroot`.

How to publish (default behavior):

```bash
cd backend
dotnet publish -c Release -o publish_output
```

How to skip the frontend build (e.g., if your CI builds frontend separately):

```bash
dotnet publish -c Release -o publish_output -p:SkipFrontendBuild=true
```

Notes:
- Make sure Node.js and npm are available on the machine where `dotnet publish` runs.
- If you prefer `npm install` instead of `npm ci`, update the target in `backend.csproj` accordingly.
