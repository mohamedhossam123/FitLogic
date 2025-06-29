// ProfileCard.tsx
import React, { useMemo, useCallback, useRef, useState, useEffect } from "react";
import "./ProfileCard.css"; 
interface ProfileCardProps {
  avatarUrl?: string;
  iconUrl?: string; 
  grainUrl?: string; 
  behindGradient?: string;
  innerGradient?: string;
  showBehindGradient?: boolean;
  className?: string;
  enableTilt?: boolean;
  miniAvatarUrl?: string; 
  name?: string;
  title?: string;
  handle?: string;
  status?: string; 
  contactText?: string;
  showUserInfo?: boolean;
  onContactClick?: () => void;
  linkedinUrl?: string;
  githubUrl?: string;
  email?: string;
}

const DEFAULT_BEHIND_GRADIENT =
  "radial-gradient(35% 52% at 55% 20%,#8b5cf6c4 0%,#07080800 100%), radial-gradient(100% 100% at 50% 50%,#8a2be2ff 1%,#07080800 76%), conic-gradient(from 124deg at 50% 50%,#9933ffff 0%,#6a0dadff 40%,#6a0dadff 60%,#9933ffff 100%)";

const DEFAULT_INNER_GRADIENT =
  "linear-gradient(145deg, #3a2e4c8c 0%, #8a2be244 100%)"; // Darker, purple-tinted inner gradient

const ProfileCardComponent: React.FC<ProfileCardProps> = ({
  avatarUrl = "https://placehold.co/120x120/8A2BE2/FFFFFF?text=MH", // Default placeholder
  iconUrl,
  grainUrl,
  behindGradient,
  innerGradient,
  showBehindGradient = true,
  className = "",
  enableTilt = false,
  name = "Mohamed Hossam",
  title = "Software Engineer",
  handle = "mohamedhossam123",
  contactText = "Email Me",
  email = "mohamedhossam25709@gmail.com",
  showUserInfo = true,
  onContactClick,
  linkedinUrl,
  githubUrl,
}) => {
  const cardRef = useRef<HTMLDivElement>(null);
  const [rotateX, setRotateX] = useState(0);
  const [rotateY, setRotateY] = useState(0);
  const [pointerX, setPointerX] = useState(50); 
  const [pointerY, setPointerY] = useState(50); 
  const handleMouseMove = useCallback((e: React.MouseEvent<HTMLDivElement>) => {
    if (!enableTilt || !cardRef.current) return;

    const { left, top, width, height } = cardRef.current.getBoundingClientRect();
    const centerX = left + width / 2;
    const centerY = top + height / 2;

    const mouseX = e.clientX;
    const mouseY = e.clientY;

    const percentX = (mouseX - left) / width; 
    const percentY = (mouseY - top) / height; 

    setPointerX(percentX * 100);
    setPointerY(percentY * 100);

    const newRotateX = (percentY - 0.5) * 20; 
    const newRotateY = -(percentX - 0.5) * 20; 

    setRotateX(newRotateX);
    setRotateY(newRotateY);
  }, [enableTilt]);

  const handleMouseLeave = useCallback(() => {
    if (!enableTilt) return;
    setRotateX(0);
    setRotateY(0);
    setPointerX(50);
    setPointerY(50);
  }, [enableTilt]);

  const cardStyle = useMemo(
    () =>
      ({
        "--icon": iconUrl ? `url(${iconUrl})` : "none",
        "--grain": grainUrl ? `url(${grainUrl})` : "none",
        "--behind-gradient": showBehindGradient
          ? behindGradient ?? DEFAULT_BEHIND_GRADIENT
          : "none",
        "--inner-gradient": innerGradient ?? DEFAULT_INNER_GRADIENT,
        "--rotate-x": `${rotateX}deg`,
        "--rotate-y": `${rotateY}deg`,
        "--pointer-x": `${pointerX}%`,
        "--pointer-y": `${pointerY}%`,
      }) as React.CSSProperties,
    [iconUrl, grainUrl, showBehindGradient, behindGradient, innerGradient, rotateX, rotateY, pointerX, pointerY]
  );

  const handleContactClick = useCallback(() => {
    if (onContactClick) {
      onContactClick();
    } else if (email) {
      window.location.href = `mailto:${email}?subject=Contact%20from%20FitLogic%20Profile`;
    }
  }, [onContactClick, email]);

  return (
    <div
      ref={cardRef}
      className={`pc-card-wrapper ${className} ${enableTilt ? 'tilt-active' : ''}`.trim()}
      style={cardStyle}
      onMouseMove={handleMouseMove}
      onMouseLeave={handleMouseLeave}
      onMouseEnter={() => enableTilt && cardRef.current?.classList.add('active')}
    >
      <section className="pc-card">
        <div className="pc-inside">
          <div className="pc-shine" />
          <div className="pc-glare" />

          {/* Avatar */}
          <img
            className="pc-avatar"
            src={avatarUrl}
            alt={`${name || "User"} avatar`}
            loading="lazy"
          />

          {/* Details (Name & Title) */}
          <div className="pc-details">
            <h3>{name}</h3>
            <p>{title}</p>
          </div>

          {/* Social Links */}
          {(githubUrl || linkedinUrl) && (
            <div className="pc-social-links">
              {githubUrl && (
                <a
                  href={githubUrl}
                  target="_blank"
                  rel="noopener noreferrer"
                  aria-label="GitHub Profile"
                >
                  <svg
                    className="pc-social-icon"
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="currentColor"
                  >
                    <path d="M12 0c-6.626 0-12 5.373-12 12 0 5.302 3.438 9.8 8.207 11.387.599.111.793-.261.793-.577v-2.234c-3.338.726-4.033-1.416-4.033-1.416-.546-1.387-1.333-1.756-1.333-1.756-1.087-.731.084-.716.084-.716 1.205.082 1.838 1.215 1.838 1.215 1.07 1.835 2.809 1.305 3.493.998.108-.776.418-1.305.762-1.604-2.665-.305-5.467-1.334-5.467-5.931 0-1.311.469-2.381 1.236-3.221-.124-.303-.535-1.524.118-3.176 0 0 1.008-.322 3.301 1.23.957-.266 1.983-.399 3.003-.404 1.02.005 2.046.138 3.003.404 2.293-1.552 3.301-1.23 3.301-1.23.653 1.653.242 2.874.118 3.176.77.84 1.235 1.911 1.235 3.221 0 4.609-2.807 5.624-5.479 5.921.43.372.823 1.102.823 2.222v3.293c0 .319.192.694.801.576 4.765-1.589 8.196-6.086 8.196-11.386C24 5.373 18.627 0 12 0Z" />
                  </svg>
                </a>
              )}
              {linkedinUrl && (
                <a
                  href={linkedinUrl}
                  target="_blank"
                  rel="noopener noreferrer"
                  aria-label="LinkedIn Profile"
                >
                  <svg
                    className="pc-social-icon"
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="currentColor"
                  >
                    <path d="M20.447 20.452h-3.52v-5.593c0-1.336-.027-3.06-1.86-3.06-1.867 0-2.153 1.448-2.153 2.96v5.693h-3.518V9.206h3.382l.163 1.644h.043c.472-.857 1.623-1.866 3.344-1.866 3.582 0 4.243 2.364 4.243 5.424v6.204zM3.94 6.7c-1.22 0-2.208-.993-2.208-2.21 0-1.216.988-2.208 2.208-2.208s2.208.992 2.208 2.208c0 1.217-.988 2.21-2.208 2.21zm1.758 13.752h-3.52V9.206h3.52v11.246z" />
                  </svg>
                </a>
              )}
            </div>
          )}

    
          {/* User Info (Handle & Contact Button) */}
          {showUserInfo && (
            <div className="pc-user-info">
              <div className="pc-handle">@{handle}</div>
              {email && (
  <a
    href={`mailto:${email}?subject=Contact%20from%20FitLogic%20Profile`}
    className="flex items-center gap-2 text-sm text-gray-300 hover:text-white transition"
    aria-label={`Email ${name || "user"}`}
  >
    <svg
      xmlns="http://www.w3.org/2000/svg"
      className="h-5 w-5 text-purple-400"
      fill="none"
      viewBox="0 0 24 24"
      stroke="currentColor"
    >
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 12l-4 4m0 0l-4-4m4 4V8m8 4a9 9 0 11-18 0 9 9 0 0118 0z" />
    </svg>
    <span>{email}</span>
  </a>
)}


            </div>
          )}
        </div>
      </section>
    </div>
  );
};

const ProfileCard = React.memo(ProfileCardComponent);
export default ProfileCard;