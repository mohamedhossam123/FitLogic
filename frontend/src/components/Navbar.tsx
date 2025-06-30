'use client';

import React, { useCallback, useEffect, useRef, useState } from 'react';

interface NavbarProps {
  activeSection: string;
  setActiveSection: (section: string) => void;
}

const navItems = [
  { label: 'Home', href: '#home' },
  { label: 'Workout', href: '#workout' },
  { label: 'Calorie Calc', href: '#calorie-calc' },
  { label: 'About', href: '#about' },
];

const Navbar: React.FC<NavbarProps> = ({ activeSection, setActiveSection }) => {
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const navRef = useRef<HTMLElement>(null);

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  const handleClickOutside = useCallback((event: MouseEvent) => {
    if (navRef.current && !navRef.current.contains(event.target as Node) && isMobileMenuOpen) {
      setIsMobileMenuOpen(false);
    }
  }, [isMobileMenuOpen]);

  useEffect(() => {
    if (isMobileMenuOpen) {
      document.addEventListener('mousedown', handleClickOutside);
      document.body.style.overflow = 'hidden';
    } else {
      document.removeEventListener('mousedown', handleClickOutside);
      document.body.style.overflow = 'unset';
    }
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
      document.body.style.overflow = 'unset';
    };
  }, [isMobileMenuOpen, handleClickOutside]);

  const handleNavLinkClick = (e: React.MouseEvent<HTMLAnchorElement>, href: string) => {
    e.preventDefault();
    const targetId = href.substring(1);
    const targetElement = document.getElementById(targetId);
    if (targetElement) {
      const headerOffset = 70;
      const elementPosition = targetElement.getBoundingClientRect().top + window.scrollY;
      const offsetPosition = elementPosition - headerOffset;
      window.scrollTo({ top: offsetPosition, behavior: 'smooth' });
      setIsMobileMenuOpen(false);
    }
  };

  return (
    <header ref={navRef} className="flex w-full h-[70px] items-center justify-between px-4 md:px-10 lg:px-[100px] py-0 fixed top-0 left-0 z-50 backdrop-blur-md bg-black/60 shadow-lg">
      <div className="flex items-center gap-2">
        <img className="w-[45px] h-[45px] object-contain" alt="FitLogic Logo" src="/logo-nav--removebg-preview.png" />
        <span className="font-dmsans font-extrabold text-white text-3xl tracking-tight">FitLogic</span>
      </div>

      <button
        className="lg:hidden text-white focus:outline-none focus:ring-2 focus:ring-purple-400 rounded p-1"
        onClick={toggleMobileMenu}
        aria-label="Toggle navigation menu"
      >
        <svg className="w-8 h-8 transition-transform duration-300 transform" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          {isMobileMenuOpen ? (
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
          ) : (
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h16" />
          )}
        </svg>
      </button>

      {/* Desktop Nav */}
      <nav className="hidden lg:flex items-center gap-8">
        {navItems.map((item, index) => (
          <a
            key={index}
            href={item.href}
            onClick={(e) => handleNavLinkClick(e, item.href)}
            className={`font-courier-prime font-medium text-lg leading-4 relative group
                        hover:text-purple-400 transition-colors duration-300
                        ${activeSection === item.href.substring(1) ? 'text-purple-400' : 'text-gray-400'}`}
          >
            {item.label}
            <span
              className={`absolute bottom-0 left-0 w-full h-0.5 bg-purple-400 transform scale-x-0 group-hover:scale-x-100 transition-transform duration-300 origin-left
                        ${activeSection === item.href.substring(1) ? 'scale-x-100' : ''}`}
            ></span>
          </a>
        ))}
      </nav>

      {isMobileMenuOpen && (
        <div className="lg:hidden absolute top-[70px] left-0 w-full bg-black/90 flex flex-col items-center py-6 space-y-6 shadow-xl animate-fade-in-down">
          {navItems.map((item, index) => (
            <a
              key={index}
              href={item.href}
              onClick={(e) => handleNavLinkClick(e, item.href)}
              className={`font-courier-prime font-bold text-xl hover:text-purple-400 transition-colors w-full text-center py-3
                          ${activeSection === item.href.substring(1) ? 'text-purple-400 bg-gray-800' : 'text-gray-300'}`}
            >
              {item.label}
            </a>
          ))}
        </div>
      )}
    </header>
  );
};

export default Navbar;
