function run()
  local rv = PackingSlip("Shipping")
  
  if (itemtype ~= "book") then
    rv = rv .. " " .. AddCommission("Commission Payment")
  end
  
  return rv
end

