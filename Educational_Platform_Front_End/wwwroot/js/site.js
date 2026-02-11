// ============================================
// ENTERPRISE SECTION - INTERACTIVE FEATURES
// Complete JavaScript for Enterprise Landing Page
// ============================================

document.addEventListener('DOMContentLoaded', function () {
    console.log('Enterprise page initialized');

    // ============================================
    // SCROLL ANIMATIONS
    // ============================================
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -100px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, observerOptions);

    // Animate sections on scroll
    const animatedSections = document.querySelectorAll('.feature-showcase, .testimonial-card-enterprise, .pricing-card');
    animatedSections.forEach((section, index) => {
        section.style.opacity = '0';
        section.style.transform = 'translateY(40px)';
        section.style.transition = `all 0.8s cubic-bezier(0.4, 0, 0.2, 1) ${index * 0.1}s`;
        observer.observe(section);
    });

    // ============================================
    // STATS COUNTER ANIMATION
    // ============================================
    function animateCounter(element, target, duration = 2000) {
        const start = 0;
        const increment = target / (duration / 16);
        let current = start;

        const timer = setInterval(() => {
            current += increment;
            if (current >= target) {
                element.textContent = formatStatValue(target);
                clearInterval(timer);
            } else {
                element.textContent = formatStatValue(Math.floor(current));
            }
        }, 16);
    }

    function formatStatValue(value) {
        if (value >= 1000) {
            return (value / 1000).toFixed(0) + 'K+';
        }
        return value.toString();
    }

    // Trigger counter animation when stats come into view
    const statsObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting && !entry.target.dataset.animated) {
                entry.target.dataset.animated = 'true';
                const statValues = entry.target.querySelectorAll('.enterprise-stat__value');

                // Animate first stat to 500
                if (statValues[0]) {
                    statValues[0].textContent = '0';
                    animateCounter(statValues[0], 500);
                }
            }
        });
    }, { threshold: 0.5 });

    const statsContainer = document.querySelector('.enterprise-hero__stats');
    if (statsContainer) {
        statsObserver.observe(statsContainer);
    }

    // ============================================
    // PRICING CARDS INTERACTION
    // ============================================
    const pricingCards = document.querySelectorAll('.pricing-card');
    pricingCards.forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.zIndex = '10';
        });

        card.addEventListener('mouseleave', function () {
            this.style.zIndex = '1';
        });
    });

    // ============================================
    // BUTTON RIPPLE EFFECT
    // ============================================
    const allButtons = document.querySelectorAll('.btn-enterprise, .btn-pricing');
    allButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            // Create ripple element
            const ripple = document.createElement('span');
            ripple.style.position = 'absolute';
            ripple.style.borderRadius = '50%';
            ripple.style.backgroundColor = 'rgba(255, 255, 255, 0.6)';
            ripple.style.width = '100px';
            ripple.style.height = '100px';
            ripple.style.marginLeft = '-50px';
            ripple.style.marginTop = '-50px';
            ripple.style.left = e.clientX - this.getBoundingClientRect().left + 'px';
            ripple.style.top = e.clientY - this.getBoundingClientRect().top + 'px';
            ripple.style.animation = 'ripple 0.6s';

            // Set button to relative position
            this.style.position = 'relative';
            this.style.overflow = 'hidden';

            // Add ripple
            this.appendChild(ripple);

            // Remove ripple after animation
            setTimeout(() => ripple.remove(), 600);
        });
    });

    // ============================================
    // PARALLAX EFFECT FOR HERO BACKGROUND
    // ============================================
    const heroGradient = document.querySelector('.enterprise-hero__gradient');
    const heroGrid = document.querySelector('.enterprise-hero__grid');

    window.addEventListener('scroll', () => {
        const scrolled = window.pageYOffset;
        const rate = scrolled * 0.3;

        if (heroGradient) {
            heroGradient.style.transform = `translateY(${rate}px)`;
        }

        if (heroGrid) {
            heroGrid.style.transform = `translateY(${rate * 0.5}px)`;
        }
    });

    // ============================================
    // FLOATING CARD ANIMATION PAUSE ON HOVER
    // ============================================
    const floatingCard = document.querySelector('.enterprise-card--floating');
    if (floatingCard) {
        floatingCard.addEventListener('mouseenter', function () {
            this.style.animationPlayState = 'paused';
        });

        floatingCard.addEventListener('mouseleave', function () {
            this.style.animationPlayState = 'running';
        });
    }

    // ============================================
    // CHART BARS ANIMATION ON SCROLL
    // ============================================
    const chartBars = document.querySelectorAll('.chart-bar');
    const chartObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.animationPlayState = 'running';
            }
        });
    }, { threshold: 0.5 });

    chartBars.forEach(bar => {
        bar.style.animationPlayState = 'paused';
        chartObserver.observe(bar);
    });

    // ============================================
    // FEATURE CARDS TILT EFFECT
    // ============================================
    const featureCards = document.querySelectorAll('.analytics-feature');
    featureCards.forEach(card => {
        card.addEventListener('mousemove', function (e) {
            const rect = this.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            const centerX = rect.width / 2;
            const centerY = rect.height / 2;

            const rotateX = (y - centerY) / 10;
            const rotateY = (centerX - x) / 10;

            this.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) translateX(8px)`;
        });

        card.addEventListener('mouseleave', function () {
            this.style.transform = 'perspective(1000px) rotateX(0) rotateY(0) translateX(0)';
        });
    });

    // ============================================
    // TESTIMONIAL CARDS STAGGER ANIMATION
    // ============================================
    const testimonialCards = document.querySelectorAll('.testimonial-card-enterprise');
    const testimonialObserver = new IntersectionObserver((entries) => {
        entries.forEach((entry, index) => {
            if (entry.isIntersecting) {
                setTimeout(() => {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                }, index * 200);
            }
        });
    }, { threshold: 0.2 });

    testimonialCards.forEach(card => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(40px)';
        card.style.transition = 'all 0.6s cubic-bezier(0.4, 0, 0.2, 1)';
        testimonialObserver.observe(card);
    });

    // ============================================
    // TRUST LOGO SCROLL ANIMATION
    // ============================================
    const trustLogos = document.querySelectorAll('.trust-logo');
    const logoObserver = new IntersectionObserver((entries) => {
        entries.forEach((entry, index) => {
            if (entry.isIntersecting) {
                setTimeout(() => {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                }, index * 100);
            }
        });
    }, { threshold: 0.5 });

    trustLogos.forEach(logo => {
        logo.style.opacity = '0';
        logo.style.transform = 'translateY(20px)';
        logo.style.transition = 'all 0.5s cubic-bezier(0.4, 0, 0.2, 1)';
        logoObserver.observe(logo);
    });

    // ============================================
    // PROGRESS RING ANIMATION
    // ============================================
    const progressRing = document.querySelector('.progress-ring__progress');
    const progressObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Trigger animation by changing CSS variable
                setTimeout(() => {
                    entry.target.style.strokeDashoffset = 'calc(283 * (1 - var(--progress)))';
                }, 200);
            }
        });
    }, { threshold: 0.5 });

    if (progressRing) {
        progressRing.style.strokeDashoffset = '283';
        progressObserver.observe(progressRing);
    }

    // ============================================
    // ANALYTICS DASHBOARD METRICS COUNTER
    // ============================================
    const metricValues = document.querySelectorAll('.metric-card__value');
    const metricsObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting && !entry.target.dataset.animated) {
                entry.target.dataset.animated = 'true';
                const targetText = entry.target.textContent;

                if (targetText.includes('%')) {
                    const targetValue = parseFloat(targetText);
                    let current = 0;
                    const increment = targetValue / 50;

                    const timer = setInterval(() => {
                        current += increment;
                        if (current >= targetValue) {
                            entry.target.textContent = targetValue.toFixed(1) + '%';
                            clearInterval(timer);
                        } else {
                            entry.target.textContent = current.toFixed(1) + '%';
                        }
                    }, 30);
                } else if (targetText.includes(',')) {
                    const targetValue = parseInt(targetText.replace(/,/g, ''));
                    let current = 0;
                    const increment = targetValue / 50;

                    const timer = setInterval(() => {
                        current += increment;
                        if (current >= targetValue) {
                            entry.target.textContent = targetValue.toLocaleString();
                            clearInterval(timer);
                        } else {
                            entry.target.textContent = Math.floor(current).toLocaleString();
                        }
                    }, 30);
                }
            }
        });
    }, { threshold: 0.5 });

    metricValues.forEach(metric => metricsObserver.observe(metric));

    // ============================================
    // CTA BACKGROUND GLOW MOUSE FOLLOW
    // ============================================
    const ctaSection = document.querySelector('.enterprise-cta');
    const ctaGlows = document.querySelectorAll('.enterprise-cta__glow');

    if (ctaSection) {
        ctaSection.addEventListener('mousemove', function (e) {
            const rect = this.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            ctaGlows.forEach((glow, index) => {
                const speed = (index + 1) * 0.05;
                const newX = x * speed;
                const newY = y * speed;

                glow.style.transition = 'transform 0.3s ease-out';
                glow.style.transform = `translate(${newX}px, ${newY}px)`;
            });
        });
    }

    // ============================================
    // SMOOTH SCROLL FOR ANCHOR LINKS
    // ============================================
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // ============================================
    // FEATURE SHOWCASE REVEAL ON SCROLL
    // ============================================
    const featureShowcases = document.querySelectorAll('.feature-showcase');
    const featureObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('is-visible');
            }
        });
    }, { threshold: 0.2 });

    featureShowcases.forEach(feature => {
        featureObserver.observe(feature);
    });

    // ============================================
    // PRICING CARD SELECTION HIGHLIGHT
    // ============================================
    pricingCards.forEach(card => {
        const button = card.querySelector('.btn-pricing');
        if (button) {
            button.addEventListener('click', function () {
                // Remove active class from all cards
                pricingCards.forEach(c => c.classList.remove('is-selected'));
                // Add active class to clicked card
                card.classList.add('is-selected');
            });
        }
    });

    // ============================================
    // TESTIMONIAL METRIC COUNTER
    // ============================================
    const testimonialMetrics = document.querySelectorAll('.testimonial-metric__value');
    const testimonialMetricObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting && !entry.target.dataset.counted) {
                entry.target.dataset.counted = 'true';
                const text = entry.target.textContent;

                // Handle percentage values
                if (text.includes('%')) {
                    const value = parseInt(text);
                    let current = 0;
                    const increment = value / 30;

                    const timer = setInterval(() => {
                        current += increment;
                        if (current >= value) {
                            entry.target.textContent = value + '%';
                            clearInterval(timer);
                        } else {
                            entry.target.textContent = Math.floor(current) + '%';
                        }
                    }, 40);
                }
            }
        });
    }, { threshold: 0.5 });

    testimonialMetrics.forEach(metric => {
        testimonialMetricObserver.observe(metric);
    });

    // ============================================
    // KEYBOARD NAVIGATION FOR BUTTONS
    // ============================================
    allButtons.forEach(button => {
        button.addEventListener('keydown', function (e) {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                this.click();
            }
        });
    });

    // ============================================
    // DYNAMIC YEAR IN FOOTER (if applicable)
    // ============================================
    const yearElements = document.querySelectorAll('.current-year');
    const currentYear = new Date().getFullYear();
    yearElements.forEach(element => {
        element.textContent = currentYear;
    });

    // ============================================
    // MOBILE NAVIGATION TOGGLE
    // ============================================
    const mobileMenuBtn = document.querySelector('.mobile-menu-btn, .nav-toggle');
    const mobileMenu = document.querySelector('.mobile-nav');

    if (mobileMenuBtn && mobileMenu) {
        mobileMenuBtn.addEventListener('click', () => {
            const isOpen = mobileMenu.classList.toggle('is-open');
            mobileMenu.classList.toggle('active');
            mobileMenuBtn.setAttribute('aria-expanded', isOpen);
        });
    }

    // Close mobile menu when clicking outside
    document.addEventListener('click', (e) => {
        if (mobileMenu && mobileMenu.classList.contains('is-open')) {
            if (!mobileMenu.contains(e.target) && !mobileMenuBtn.contains(e.target)) {
                mobileMenu.classList.remove('is-open');
                mobileMenu.classList.remove('active');
                mobileMenuBtn.setAttribute('aria-expanded', 'false');
            }
        }
    });

    // Dashboard Sidebar Toggle
    const sidebarToggle = document.querySelector('.sidebar-toggle, .admin-sidebar-toggle');
    const sidebar = document.querySelector('.sidebar, .admin-sidebar, .admin-layout__sidebar');

    if (sidebarToggle && sidebar) {
        sidebarToggle.addEventListener('click', () => {
            const isOpen = sidebar.classList.toggle('is-active') || sidebar.classList.toggle('admin-layout__sidebar--mobile-open');
            sidebarToggle.setAttribute('aria-expanded', isOpen);
        });
    }

    console.log('All enterprise interactions initialized successfully');
});

// ============================================
// PERFORMANCE OPTIMIZATION - LAZY LOADING
// ============================================
if ('IntersectionObserver' in window) {
    const lazyImages = document.querySelectorAll('img[data-src]');
    const imageObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.removeAttribute('data-src');
                imageObserver.unobserve(img);
            }
        });
    });

    lazyImages.forEach(img => imageObserver.observe(img));
}

// ============================================
// UTILITY FUNCTIONS
// ============================================

// Debounce function for scroll events
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Throttle function for mouse events
function throttle(func, limit) {
    let inThrottle;
    return function (...args) {
        if (!inThrottle) {
            func.apply(this, args);
            inThrottle = true;
            setTimeout(() => inThrottle = false, limit);
        }
    };
}