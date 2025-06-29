'use client';
import React, { useState, useEffect } from 'react';
import { LandingPage } from '@/components/LandingPage';
import LoadingScreen from '@/components/LoadingScreen'; 

export default function Home() {
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsLoading(false); 
    }, 4000); 
    return () => clearTimeout(timer);
  }, []); 

  return (
    <>
      {isLoading && <LoadingScreen />}
      {!isLoading && <LandingPage />}
    </>
  );
}
