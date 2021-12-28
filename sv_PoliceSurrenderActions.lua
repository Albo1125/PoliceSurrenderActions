function backcuffsfunc(source, args, rawCommand)
	local tG = source
	if tablelength(args) > 0 then
		if args[1] ~= nil then
			tG = tonumber(args[1])				
		end
	end
	if GetPlayerName(tG) ~= nil then
		TriggerClientEvent("PSA:TCfs", tG, source, false)
	end	
end

RegisterCommand('cuff', backcuffsfunc, false)
RegisterCommand('c', backcuffsfunc, false)

RegisterServerEvent('PSA:svCfs')
AddEventHandler('PSA:svCfs', function(playerid, target)
	TriggerClientEvent("PSA:TCfs", target, playerid, false)
end)

function frontcuffsfunc(source, args, rawCommand)
	local tG = source
	if tablelength(args) > 0 then
		if args[1] ~= nil then
			tG = tonumber(args[1])				
		end
	end
	if GetPlayerName(tG) ~= nil then
		TriggerClientEvent("PSA:TCfs", tG, source, true)
	end	
end

RegisterCommand('frontcuff', frontcuffsfunc, false)
RegisterCommand('fc', frontcuffsfunc, false)

RegisterServerEvent('PSA:svFCfs')
AddEventHandler('PSA:svFCfs', function(playerid, target)
	TriggerClientEvent("PSA:TCfs", target, playerid, true)
end)

function dragfunc(source, args, rawCommand)
	local tG = source
	if tablelength(args) > 0 then
		if args[1] ~= nil then
			tG = tonumber(args[1])	
			if GetPlayerName(tG) ~= nil then
				TriggerClientEvent("PSA:DRAG", tG, source)
			end
		end
	end	
end

RegisterCommand('drag', dragfunc, false)
RegisterCommand('d', dragfunc, false)

RegisterServerEvent('PSA:svDRAG')
AddEventHandler('PSA:svDRAG', function(playerid, target)
	TriggerClientEvent("PSA:DRAG", target, playerid)
end)

function stopdragfunc(source, args, rawCommand)
	TriggerClientEvent("PSA:STOPDRAG", source)
end

RegisterCommand('stopdrag', stopdragfunc, false)

function handsupfunc(source, args, rawCommand)
	TriggerClientEvent("PSA:THU", source)
end

RegisterCommand('handsup', handsupfunc, false)
RegisterCommand('hu', handsupfunc, false)


function kneelfunc(source, args, rawCommand)
	TriggerClientEvent("PSA:TKN", source)
end

RegisterCommand('kneel', kneelfunc, false)

function dropweaponfunc(source, args, rawCommand)
	TriggerClientEvent("PSA:DRW", source)
end

RegisterCommand('dropweapon', dropweaponfunc, false)

function facedownfunc(source, args, rawCommand)
	TriggerClientEvent("PSA:FD", source)
end

RegisterCommand('facedown', facedownfunc, false)
RegisterCommand('fd', facedownfunc, false)

RegisterServerEvent('PSA:TooFarAway')
AddEventHandler('PSA:TooFarAway', function(cuffer)
	TriggerClientEvent('chatMessage', cuffer, 'SYSTEM', {0,0,0}, "You're too far away from that player.")
end)

function tablelength(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end