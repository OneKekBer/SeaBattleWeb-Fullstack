import { StrictMode, Suspense } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { BrowserRouter } from 'react-router-dom'
import { Provider } from 'react-redux'
import { store } from './store/Store.ts'
import { CookiesProvider } from 'react-cookie'

createRoot(document.getElementById('root')!).render(
	<StrictMode>
		<BrowserRouter>
			<CookiesProvider>
				<Provider store={store}>
					<Suspense>
						<App />
					</Suspense>
				</Provider>
			</CookiesProvider>
		</BrowserRouter>
	</StrictMode>
)
