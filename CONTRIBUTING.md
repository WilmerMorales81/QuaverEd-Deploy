# Contributing to QuaverEd

Thank you for your interest in contributing to the QuaverEd project! This document provides guidelines and information for contributors.

## Getting Started

### Prerequisites

Before contributing, ensure you have:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)
- [Visual Studio Code](https://code.visualstudio.com/) (recommended)

### Development Setup

1. **Fork and Clone**
   ```bash
   git clone https://github.com/your-username/QuaverEd_Project.git
   cd QuaverEd_Project
   ```

2. **Start Database**
   ```bash
   docker-compose up -d
   ```

3. **Run Migrations**
   ```bash
   cd QuaverEd.API
   dotnet ef database update
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

## Development Guidelines

### Code Style

- Follow C# naming conventions (PascalCase for public members, camelCase for private)
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods focused and single-purpose

### Project Structure

```
QuaverEd.API/
├── Controllers/          # API endpoints
├── Data/               # Database context and configuration
├── Models/             # Entity models
├── Migrations/         # Database migrations
└── seed_data.sql       # Sample data
```

### Database Changes

When modifying the database schema:

1. Create a new migration:
   ```bash
   dotnet ef migrations add YourMigrationName
   ```

2. Update the database:
   ```bash
   dotnet ef database update
   ```

3. Update sample data if needed

## Testing

### Running Tests

The project includes comprehensive HTTP test files:

- `api_tests.http`: Complete test suite for all endpoints
- Use VS Code REST Client extension or similar tools

### Test Coverage

Ensure your changes include tests for:

- Happy path scenarios
- Edge cases and error conditions
- Input validation
- Performance considerations

## Pull Request Process

### Before Submitting

1. **Fork the repository** and create a feature branch
2. **Make your changes** following the coding guidelines
3. **Test thoroughly** using the provided test files
4. **Update documentation** if needed
5. **Commit with clear messages**

### Pull Request Guidelines

1. **Clear Title**: Describe what the PR does
2. **Detailed Description**: Explain the changes and why they're needed
3. **Reference Issues**: Link to any related issues
4. **Test Results**: Include test results or screenshots
5. **Breaking Changes**: Clearly mark any breaking changes

### Commit Message Format

```
type(scope): brief description

Detailed description of changes

Fixes #issue-number
```

Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

## Code Review Process

### For Contributors

- Respond to feedback promptly
- Make requested changes
- Ask questions if something is unclear
- Keep PRs focused and manageable

### For Reviewers

- Be constructive and helpful
- Test the changes locally
- Check for security implications
- Verify documentation updates

## Issue Reporting

### Bug Reports

Include:

- **Environment**: OS, .NET version, Docker version
- **Steps to Reproduce**: Clear, numbered steps
- **Expected Behavior**: What should happen
- **Actual Behavior**: What actually happens
- **Screenshots**: If applicable
- **Logs**: Any relevant error messages

### Feature Requests

Include:

- **Use Case**: Why is this feature needed?
- **Proposed Solution**: How should it work?
- **Alternatives**: Other approaches considered
- **Additional Context**: Any other relevant information

## Development Workflow

### Branch Naming

- `feature/description`: New features
- `bugfix/description`: Bug fixes
- `hotfix/description`: Critical fixes
- `docs/description`: Documentation updates

### Release Process

1. **Version Bumping**: Update version numbers
2. **Changelog**: Update CHANGELOG.md
3. **Testing**: Comprehensive testing
4. **Documentation**: Update all relevant docs
5. **Release Notes**: Clear release notes

## Code of Conduct

### Our Pledge

We are committed to providing a welcoming and inclusive environment for all contributors.

### Expected Behavior

- Use welcoming and inclusive language
- Be respectful of differing viewpoints
- Accept constructive criticism gracefully
- Focus on what's best for the community
- Show empathy towards other community members

### Unacceptable Behavior

- Harassment, trolling, or inflammatory comments
- Personal attacks or political discussions
- Public or private harassment
- Publishing private information without permission
- Any conduct inappropriate in a professional setting

## Getting Help

### Resources

- **Documentation**: Check README.md and API_DOCUMENTATION.md
- **Issues**: Search existing issues before creating new ones
- **Discussions**: Use GitHub Discussions for questions

### Contact

- **Issues**: Create a GitHub issue
- **Discussions**: Use GitHub Discussions
- **Email**: [Contact information if available]

## Recognition

Contributors will be recognized in:

- CONTRIBUTORS.md file
- Release notes
- Project documentation

Thank you for contributing to QuaverEd! 🎵
