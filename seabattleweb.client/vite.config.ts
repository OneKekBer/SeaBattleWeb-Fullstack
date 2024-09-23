// vite.config.ts or vite.config.js
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

export default defineConfig({
	plugins: [react()],
	resolve: {
		alias: {
			interfaces: path.resolve(__dirname, 'src/interfaces'),
			store: path.resolve(__dirname, 'src/store'),
			shared: path.resolve(__dirname, 'src/shared'),
			pages: path.resolve(__dirname, 'src/pages'),
			public: path.resolve(__dirname, 'public/'),
			icons: path.resolve(__dirname, 'public/icons'),
		},
	},
})
