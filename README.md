# DevOPs Assignment (Hands-On DevOps: CI/CD | LSPP 2025)

[![Build & Test](https://github.com/OWNER/REPO/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/OWNER/REPO/actions/workflows/build.yml)

A tiny .NET 8 console app packaged with Docker and wired to GitHub Actions for CI (build + test) and optional Release automation.

## Project layout
- `DevOPs_Assignment/` – .NET 8 console app
- `DevOPs_Assignment.Tests/` – xUnit tests
- `DevOPs_Assignment/Dockerfile` – multi-stage Dockerfile (build → publish → runtime)
- `.github/workflows/build.yml` – CI workflow: test then docker build
- `.github/workflows/release.yml` – Release workflow: tag-triggered image build → Trivy scan → push to GHCR → GitHub Release
- `DEBUGGING.md` – Pipeline explanation and break/fix notes
- `.SUBMISSION.md` – Submission links per assignment

## Quick start (local)
- Prereqs: .NET 8 SDK, Docker

Build and run app:

```cmd
cd DevOPs
dotnet restore DevOPs.sln
dotnet build DevOPs.sln -c Release
 dotnet run --project DevOPs_Assignment/DevOPs_Assignment.csproj -c Release
```

Run tests:

```cmd
dotnet test DevOPs.sln -c Release --verbosity normal
```

Build Docker image:

```cmd
docker build -f DevOPs_Assignment/Dockerfile -t devops-assignment:local .
```

Run container:

```cmd
docker run --rm devops-assignment:local
```

## CI: Build & Test
- Triggers: every push to `main` and manual dispatch
- Jobs:
  - `test-job`: restore, build, test the solution
  - `build-job`: Depends on `test-job` via `needs:`; runs `docker build` to validate Dockerfile

Check `.github/workflows/build.yml` for details.

## Release automation (optional challenge)
- Trigger: pushing a git tag like `v1.0.0`
- Steps:
  1) Build Docker image (multi-stage Dockerfile)
  2) Security scan with Trivy (fails on CRITICAL)
  3) Push image to GHCR (`ghcr.io/<owner>/devops-assignment:<tag>`) using `GITHUB_TOKEN`
  4) Create GitHub Release using `softprops/action-gh-release`

See `.github/workflows/release.yml`.

### Preparing GHCR permissions
- Ensure the repo is public or grant `packages: write` permission to the workflow (already set)
- No extra secrets required; `GITHUB_TOKEN` is used for GHCR and release

## Status badge
Replace `OWNER/REPO` in the badge URL at the top with your GitHub org/user and repository name once you push.

## Notes
- Source targets .NET 8 LTS for stable CI containers
- Tests are tiny (xUnit) for pipeline verification
- Dockerfile is standard and production-friendly (runtime-only final stage)

