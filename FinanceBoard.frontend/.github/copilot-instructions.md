# Copilot Instructions for FinanceBoard.frontend

## Project Overview
- This is a React + TypeScript frontend project using Vite for fast development and builds.
- The codebase is minimal and modular, with all source code in `src/`.
- Hot Module Replacement (HMR) is enabled by default via Vite.

## Key Files & Structure
- `src/App.tsx`: Main application component. Entry point for UI logic.
- `src/main.tsx`: Bootstraps React app and renders `App`.
- `vite.config.ts`: Vite configuration, including plugins and build options.
- `eslint.config.js`: ESLint configuration. See README for expanding lint rules.
- `public/`: Static assets served at root.

## Developer Workflows
- **Start Dev Server:** `npm run dev` (Vite HMR, fast refresh)
- **Build for Production:** `npm run build`
- **Preview Production Build:** `npm run preview`
- **Lint:** `npm run lint` (uses ESLint, see config for recommended plugins)

## Patterns & Conventions
- Use functional React components and hooks.
- TypeScript is required for all source files (`.ts`, `.tsx`).
- Styles are in CSS files (`App.css`, `index.css`).
- Asset imports (SVG, images) are handled via Vite's asset pipeline.
- No custom state management or routing libraries are present by default.

## Integration Points
- Vite plugins for React (`@vitejs/plugin-react` or `@vitejs/plugin-react-swc`).
- ESLint plugins recommended: `eslint-plugin-react-x`, `eslint-plugin-react-dom` (see README for config examples).

## Extending & Customizing
- For advanced linting, update `eslint.config.js` as shown in README.
- To add new pages/components, place them in `src/` and import into `App.tsx`.
- For static assets, add to `public/` or `src/assets/`.

## Example: Adding a Component
1. Create `src/MyComponent.tsx`:
   ```tsx
   import React from 'react';
   export function MyComponent() {
     return <div>Hello from MyComponent!</div>;
   }
   ```
2. Import and use in `App.tsx`:
   ```tsx
   import { MyComponent } from './MyComponent';
   // ...existing code...
   <MyComponent />
   ```

## References
- See `README.md` for ESLint and Vite plugin details.
- Key configs: `vite.config.ts`, `eslint.config.js`, `tsconfig*.json`.

---

If any section is unclear or missing, please provide feedback to improve these instructions.