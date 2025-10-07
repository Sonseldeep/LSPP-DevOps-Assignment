 # DEBUGGING: Build Pipeline Analysis and Break/Fix

This document explains how the CI pipeline works and captures the break/fix exercise results.

## Part A: How the working pipeline from Assignment 1 works

- Trigger: On every push to `main` or manual dispatch
- Jobs:
  - `test-job`:
    - Checks out the repo
    - Sets up .NET 8
    - Restores, builds, and runs tests for the solution (`DevOPs.sln`)
  - `build-job`:
    - Depends on `test-job` via `needs: test-job` (runs only if tests pass)
    - Checks out the repo
    - Runs `docker build` using `DevOPs_Assignment/Dockerfile` to validate the Dockerfile

Key concept: `needs:` creates a dependency chain so that `build-job` won’t run if `test-job` fails. This enforces CI quality gates.

Files to view: `.github/workflows/build.yml`

## Part B: Break and Fix Challenge

1) Break it
- Edit `DevOPs_Assignment/Dockerfile` and intentionally make the base image invalid, for example:
  - Change the first line to: `FROM mcr.microsoft.com/dotnet/runtime:8.0-this-is-a-fake-tag as base`
- Commit and push to `main`.

2) Observe failure
- Go to the GitHub Actions "Actions" tab, open the failing run, and click into `build-job`.
- Capture a screenshot of the specific docker error message about pulling the image failing.
- Save the screenshot to `assets/docker-error.png` in this repo and commit it.

3) Fix it
- Revert the Dockerfile to a valid image tag (as provided in the repository’s current Dockerfile: `FROM mcr.microsoft.com/dotnet/runtime:8.0` etc.)
- Commit and push; confirm the pipeline turns green.

4) Document
- Screenshot path: `assets/docker-error.png`
- Error summary: Docker failed to pull the non-existent image tag and the build step exited with an error.
- Fix summary: Restored the correct base image tag in the `FROM` line; pipeline succeeded.

Links to include in submission:
- Failed run (broken Dockerfile): <ADD LINK TO FAILED RUN>
- Successful run (fixed Dockerfile): <ADD LINK TO SUCCESSFUL RUN>

