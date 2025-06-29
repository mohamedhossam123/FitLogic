import type { Metadata } from 'next'
import './globals.css'

export const metadata: Metadata = {
  title: 'FitLogic: Beyond Limits',
  description:
    'Unlock your full potential with cutting-edge fitness algorithms. Experience personalized workout plans and precise calorie calculations.',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <head>
        <link
          href="https://fonts.googleapis.com/css?family=Courier+Prime:700,400|Libre+Baskerville:700|DM+Sans:700"
          rel="stylesheet"
        />
        <link rel="icon" href="/logo-nav-.png" sizes="any" />
      </head>
      <body>{children}</body>
    </html>
  )
}
