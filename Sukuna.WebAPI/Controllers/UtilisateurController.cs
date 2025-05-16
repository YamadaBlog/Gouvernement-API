using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UtilisateurController(
            IUtilisateurService utilisateurService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _utilisateurService = utilisateurService;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET: api/Utilisateur
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUtilisateurs()
        {
            var utilisateurs = await _utilisateurService.GetAllUtilisateursAsync();
            var ressources = _mapper.Map<IEnumerable<Utilisateur>, IEnumerable<UtilisateurResource>>(utilisateurs);
            return Ok(ressources);
        }

        // GET: api/Utilisateur/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilisateur(int id)
        {
            var user = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (user == null) return NotFound();
            var resource = _mapper.Map<Utilisateur, UtilisateurResource>(user);
            return Ok(resource);
        }

        // POST: api/Utilisateur
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUtilisateur([FromBody] UtilisateurResource utilisateurResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var utilisateur = _mapper.Map<UtilisateurResource, Utilisateur>(utilisateurResource);
            await _utilisateurService.CreateUtilisateurAsync(utilisateur);
            if (await _utilisateurService.SaveAsync())
            {
                var result = _mapper.Map<Utilisateur, UtilisateurResource>(utilisateur);
                return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.IdUtilisateur }, result);
            }
            return BadRequest("Erreur lors de la création de l'utilisateur");
        }

        // POST: api/Utilisateur/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var utilisateur = await _utilisateurService.GetAuthauthUser(dto.Email, dto.MotDePasse);
            if (utilisateur == null)
                return Unauthorized("Email ou mot de passe incorrect.");

            // Création des claims, y compris le rôle
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateur.Email),
                new Claim(ClaimTypes.NameIdentifier, utilisateur.IdUtilisateur.ToString()),
                new Claim(ClaimTypes.Role, utilisateur.Role)
            };

            var jwtSection = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSection["ExpireMinutes"]));
            var token = new JwtSecurityToken(
                                issuer: jwtSection["Issuer"],
                                audience: jwtSection["Audience"],
                                claims: claims,
                                expires: expires,
                                signingCredentials: creds
                             );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var userResource = _mapper.Map<Utilisateur, UtilisateurResource>(utilisateur);
            return Ok(new { token = tokenString, user = userResource });
        }

        // PUT: api/Utilisateur/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(int id, [FromBody] UtilisateurResource utilisateurResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var fromDb = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (fromDb == null) return NotFound();

            _mapper.Map(utilisateurResource, fromDb);
            await _utilisateurService.UpdateUtilisateurAsync(fromDb);
            if (await _utilisateurService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la mise à jour de l'utilisateur");
        }

        // DELETE: api/Utilisateur/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var user = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (user == null) return NotFound();

            await _utilisateurService.DeleteUtilisateurAsync(id);
            if (await _utilisateurService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la suppression de l'utilisateur");
        }
    }

    /// <summary>
    /// DTO pour la requête de login
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
    }
}
