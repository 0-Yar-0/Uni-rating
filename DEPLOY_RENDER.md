# Deploy to Render

This repository includes a `Dockerfile` and a `render.yaml` manifest so you can deploy the app to Render quickly.

Two quick options:

1) Use Render's GitHub integration (recommended)
   - Go to https://dashboard.render.com and create an account (if you don't have one).
   - Select "New" → "Web Service" and connect your GitHub repository, or simply import the repo and Render will detect `render.yaml`.
   - If you import via `render.yaml`, Render will create the `university-rating` web service using the provided Dockerfile.
   - Add environment variables (in Render console) required by the app:
     - `ConnectionStrings__DefaultConnection` (Postgres connection string)
     - `Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience`
     - Any other secrets you use in `appsettings.*.json`
   - Deploy: Render will build the Docker image and run the service. The web app will be available at the provided Render URL.

## Automatic deploys from GitHub Actions (recommended)
You can set up GitHub Actions to build your Docker image, push it to GitHub Container Registry (GHCR), and trigger a Render deploy automatically.

1. Create repository secret `RENDER_DEPLOY_HOOK` with the Deploy Hook URL from Render (found in your service Settings → Deploy Hooks).
2. (Optional) Create repository secret `RENDER_SERVICE_URL` with the full URL of your deployed service (e.g. `https://university-rating.onrender.com`). If provided, the workflow will run a smoke test that polls this URL and fails the job if the service does not respond with HTTP 200 within the timeout.
3. GitHub Actions workflow `.github/workflows/render-deploy.yml` is included in this repo and will run on push to `main`. It:
   - Builds and pushes the image to `ghcr.io/<your-org>/unirating:<sha>` and `:latest`.
   - Calls the Render deploy hook with `imgURL` pointing to the newly pushed image so Render pulls and deploys it.

Notes:
- Ensure your Render service is configured as an image-backed service using the same image host/namespace (e.g. `ghcr.io/<your-org>/unirating:latest`) and that you have added a Container Registry Credential if the image is private.

### Docker Hub option
If you prefer Docker Hub instead of GHCR, the repository includes an alternative workflow `.github/workflows/render-deploy-dockerhub.yml` that builds the image and pushes it to Docker Hub.

Required repository secrets for Docker Hub workflow:
- `DOCKERHUB_USERNAME` — your Docker Hub username
- `DOCKERHUB_PASSWORD` — a Docker Hub access token (or password)
- `RENDER_DEPLOY_HOOK` — the Deploy Hook URL from Render (Settings → Deploy Hooks)
- (Optional) `RENDER_SERVICE_URL` — the public URL of your Render service (used for smoke tests)

How it works:
1. The workflow builds and pushes the image to `docker.io/<DOCKERHUB_USERNAME>/unirating:<sha>` and `:latest`.
2. It calls the Render deploy hook with `imgURL` set to the pushed image, so Render pulls and deploys the image.
3. If `RENDER_SERVICE_URL` is set, the workflow runs a smoke test that polls the service URL until it returns HTTP 200 or fails the job on timeout.

Notes for Docker Hub users:
- For public images, Render can pull without additional credentials; for private images add a Container Registry Credential in Render and provide the credential to the service.
- Make sure your Docker Hub credentials are configured as **Repository Secrets** and not committed to source control.

2) Manual Docker deployment (advanced)
   - Build the image locally:
     ```bash
     docker build -t myrepo/unirating:latest .
     ```
   - Push to a registry and deploy to your cloud provider or Render's private registry.

Notes:
- The `Dockerfile` builds the frontend first (Node) and then the backend with the frontend assets embedded into `wwwroot`.
- You can control the build via `render.yaml` or through Render's UI. If you prefer a non-Docker Render setup, you can also create a web service that runs `npm ci && npm run build && dotnet publish ...` as the build command and starts with `dotnet backend/publish_output/backend.dll`.

If you want, I can also add a GitHub Actions workflow that runs tests and optionally triggers Render deploys on push to `main` (or build the Docker image and push to a registry). Which would you prefer?